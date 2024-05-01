using System.Security.Claims;
using System.Security.Cryptography;
using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Domain.Common.Enums;
using Admission.User.Application.Context;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;
using Admission.User.Application.Options;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Admission.User.Application.Services.Impl;

public sealed class AuthService : IAuthService
{
    private readonly RefreshTokenOptions _refreshTokenOptions;
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly IUserDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(UserManager<AdmissionUser> userManager, IJwtService jwtService, IUserDbContext context,
        IOptions<RefreshTokenOptions> refreshTokenOptions)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _context = context;
        _refreshTokenOptions = refreshTokenOptions.Value;
    }

    public async Task<Result<TokenPairDto>> RegisterApplicantAsync(CreateApplicantDto dto)
    {
        var defaultRole = RoleType.Applicant.ToString();
        var user = new AdmissionUser
        {
            Email = dto.Email,
            FullName = dto.FullName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return new IdentityException(result.Errors.ToList());
        }

        await _userManager.AddToRoleAsync(user, defaultRole);
        await _context.Applicants.AddAsync(Applicant.Create(
            dto.Birthday,
            dto.PhoneNumber,
            dto.Citizenship,
            dto.Gender,
            user)
        );

        var tokenId = Guid.NewGuid();
        var refreshToken = GenerateRefreshToken();
        var accessToken = _jwtService.Generate(user, [defaultRole], tokenId);
        await SetRefreshToken(user, tokenId, refreshToken);


        await _context.SaveChangesAsync();

        return new TokenPairDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<Result<TokenPairDto>> LoginAsync(LoginCredentialsDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Email);
        if (user is not { DeleteTime: null } || !await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            return new BadRequestException("Invalid credentials");
        }

        return await RefreshTokens(user);
    }

    public async Task<Result<TokenPairDto>> RefreshAsync(RefreshDto dto)
    {
        var tokenIdentifiersResult = _jwtService.GetIdentifiersFromToken(dto.AccessToken);
        if (tokenIdentifiersResult.IsFailure)
        {
            return tokenIdentifiersResult.Exception;
        }

        var tokenIdentifiers = tokenIdentifiersResult.Value;

        var refreshToken = await _context.RefreshTokens
            .GetUndeleted()
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

        if (refreshToken == null)
        {
            return new NotFoundException("Refresh token was not found");
        }

        if (refreshToken.RefreshTokenIsExpired)
        {
            return new BadRequestException("Invalid Refresh token");
        }

        if (refreshToken.UserId != tokenIdentifiers.UserId ||
            refreshToken.AccessTokenId != tokenIdentifiers.TokenId)
        {
            return new BadRequestException("Invalid access token");
        }

        return await RefreshTokens(refreshToken.User);
    }

    public async Task<Result> LogoutAsync(Guid userId, Guid tokenId)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.AccessTokenId == tokenId);

        if (refreshToken == null)
        {
            return new NotFoundException("Refresh token was not found");
        }

        if (refreshToken.DeleteTime.HasValue)
        {
            return new BadRequestException("User has already been logged out");
        }

        refreshToken.DeleteTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<TokenPairDto> RefreshTokens(AdmissionUser user)
    {
        var tokenId = Guid.NewGuid();
        var accessToken = _jwtService.Generate(user, await _userManager.GetRolesAsync(user), tokenId);
        var refreshToken = GenerateRefreshToken();
        await SetRefreshToken(user, tokenId, refreshToken);
        await _context.SaveChangesAsync();

        return new TokenPairDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private async Task SetRefreshToken(AdmissionUser user, Guid tokenId, string refreshToken)
    {
        await _context.RefreshTokens.AddAsync(new RefreshToken
        {
            User = user,
            Token = refreshToken,
            RefreshTokenExpirationTime = DateTime.UtcNow.AddHours(_refreshTokenOptions.RefreshTokenExpirationHours)
        });
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(_refreshTokenOptions.RefreshTokenBytes));
    }
}