using Application.Features.RolePermissions.Extensions.GetAccessTokens;
using Application.Repository.Contexts;
using AutoMapper;
using CrossCuttingConcern.Exceptions.ExceptionTypes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Security.Hash;
using Security.Token;
using Security.Token.Models;

namespace Application.Features.Users.Commands.RefreshToken;

public class UserRefreshTokenCommand : IRequest<UserRefreshAccessToken>
{
    public required string RefreshToken { get; set; }
}

public class UserRefreshTokenCommandHandler : IRequestHandler<UserRefreshTokenCommand, UserRefreshAccessToken>
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHashHelper _hashHelper;
    private readonly IHttpContextAccessor _httpContext;
    private readonly ITokenHelper _tokenHelper;


    public UserRefreshTokenCommandHandler(BaseDbContext context, IMapper mapper, IHashHelper hashHelper, ITokenHelper tokenHelper, IHttpContextAccessor httpContext1)
    {
        _context = context;
        _mapper = mapper;
        _hashHelper = hashHelper;
        _tokenHelper = tokenHelper;
        _httpContext = httpContext1;
    }

    public async Task<UserRefreshAccessToken> Handle(UserRefreshTokenCommand request, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken && x.IsDeleted, cancellationToken: ct);
        if (user == null) throw new BusinessException("");


        var moduleWithPermissionsList = await _context.RolePermissions.Where(x => x.RoleId == user.RoleId)
            .SelectMany(x => x.Role.RolePermissions)
            .Where(x => x.IsDeleted)
            .GetAccessTokens(ct);

        var accessTokens = _tokenHelper.CreateAccessTokens(moduleWithPermissionsList, user.Id);


        var userRefreshAccessToken = new UserRefreshAccessToken
        {
            TokenList = accessTokens
        };

        return userRefreshAccessToken;
    }
}

public class UserRefreshAccessToken
{
    public required List<AccessTokenWithModuleIdModel> TokenList { get; set; }
}

public class UserLoginCommandValidator : AbstractValidator<UserRefreshTokenCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .Length(172);
    }
}