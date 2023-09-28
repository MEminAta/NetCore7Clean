using Application.Features.RolePermissions.Extensions.GetAccessTokens;
using Application.Features.Users._Commons;
using Application.Repository.Contexts;
using Application.Repository.Extensions.Updating;
using AutoMapper;
using Domain.Derived;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Security.Hash;
using Security.Token;
using Security.Token.Models;

namespace Application.Features.Users.Commands.Create;

public class UserCreateCommand : IRequest<UserCreatedModel>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserCreatedModel>
{
    private readonly BaseDbContext _context;
    private readonly HttpContext _httpContext;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly IHashHelper _hashHelper;
    private readonly UserBusinessRules _userBusinessRules;


    public UserCreateCommandHandler(ITokenHelper tokenHelper, IMapper mapper, IHashHelper hashHelper, IHttpContextAccessor httpContext, BaseDbContext context, UserBusinessRules userBusinessRules)
    {
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _hashHelper = hashHelper;
        _context = context;
        _userBusinessRules = userBusinessRules;
        _httpContext = httpContext.HttpContext!;
    }


    public async Task<UserCreatedModel> Handle(UserCreateCommand request, CancellationToken ct)
    {
        var user = _mapper.Map<User>(request);
        await _userBusinessRules.UserEmailCanNotDuplicated(user.Email, ct);

        var passwordModel = _hashHelper.CreateHash(request.Password);
        user.Password = passwordModel.Password;
        user.Salt = passwordModel.Salt;
        user.RoleId = 1;


        await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);

        var refreshTokenParam = new RefreshTokenModel
        {
            UId = user.Id,
            Ip = _httpContext.Connection.RemoteIpAddress!.ToString(),
            Ag = _httpContext.Request.Headers.UserAgent.ToString()
        };
        user.RefreshToken = _tokenHelper.CreateRefreshToken(refreshTokenParam);
        var updateProps = new List<string> { nameof(user.RefreshToken) };
        _context.AdvancedUpdate(user, updateProps);
        await _context.SaveChangesAsync(ct);

        var moduleWithPermissionsList = await _context.RolePermissions.Where(x => x.RoleId == user.RoleId && x.IsDeleted).GetAccessTokens(ct);
        var accessTokens = _tokenHelper.CreateAccessTokens(moduleWithPermissionsList, user.Id);

        var response = _mapper.Map<UserCreatedModel>(user);
        response.RefreshToken = user.RefreshToken;
        response.AccessTokens = accessTokens;

        return response;
    }
}

public class UserCreatedModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string RefreshToken { get; set; }
    public required List<AccessTokenWithModuleIdModel> AccessTokens { get; set; }
}

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]+$")
            .MaximumLength(50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}