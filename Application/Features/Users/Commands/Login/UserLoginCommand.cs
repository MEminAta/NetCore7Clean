using Application.Features.RolePermissions.Extensions.GetAccessTokens;
using Application.Repository.Contexts;
using Application.Repository.Extensions.Updating;
using AutoMapper;
using CrossCuttingConcern.Exceptions.ExceptionTypes;
using CrossCuttingConcern.Globalization;
using CrossCuttingConcern.Globalization.Constants;
using Domain.Derived;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Security.Hash;
using Security.Token;
using Security.Token.Models;

namespace Application.Features.Users.Commands.Login;

public class UserLoginCommand : IRequest<UserLoggedModel>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoggedModel>
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHashHelper _hashHelper;
    private readonly HttpContext _httpContext;
    private readonly ITokenHelper _tokenHelper;

    public UserLoginCommandHandler(BaseDbContext context, IHttpContextAccessor httpContextAccessor, ITokenHelper tokenHelper, IMapper mapper, IHashHelper hashHelper)
    {
        _context = context;
        _tokenHelper = tokenHelper;
        _httpContext = httpContextAccessor.HttpContext!;
        _mapper = mapper;
        _hashHelper = hashHelper;
    }

    public async Task<UserLoggedModel> Handle(UserLoginCommand request, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email & x.IsDeleted, cancellationToken: ct);
        if (user == null) throw new BusinessException(LocalizationManager.GetString(ResourceKeys.UserLoginCommand));

        var hashedPassword = _hashHelper.CreateHash(request.Password, user.Salt);
        if (user.Password.Where((t, i) => t != hashedPassword[i]).Any())
            throw new BusinessException(LocalizationManager.GetString(ResourceKeys.UserLoginCommand));

        var ipAddress = _httpContext.Connection.RemoteIpAddress!.ToString();
        var userAgent = _httpContext.Request.Headers.UserAgent.ToString();

        var refreshTokenParam = new RefreshTokenModel
        {
            UId = user.Id,
            Ip = ipAddress,
            Ag = userAgent
        };
        user.RefreshToken = _tokenHelper.CreateRefreshToken(refreshTokenParam);
        var updateProps = new List<string> { nameof(user.RefreshToken) };
        _context.AdvancedUpdate(user, updateProps);

        await _context.LoginLogs.AddAsync(new LoginLog
        {
            IpAddress = ipAddress,
            Device = null,
            UserAgent = userAgent,
            UserId = user.Id
        }, ct);
        await _context.SaveChangesAsync(ct);

        var moduleWithPermissionsList = await _context.RolePermissions.Where(x => x.RoleId == user.RoleId && x.IsDeleted).GetAccessTokens(ct);

        var accessTokens = _tokenHelper.CreateAccessTokens(moduleWithPermissionsList, user.Id);

        var userLoggedModel = _mapper.Map<UserLoggedModel>(user);
        userLoggedModel.AccessTokens = accessTokens;
        return userLoggedModel;
    }
}

public class UserLoggedModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string RefreshToken { get; set; }
    public required List<AccessTokenWithModuleIdModel> AccessTokens { get; set; }
}

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);
    }
}