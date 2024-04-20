﻿using System.Security.Cryptography;
using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Messaging;
using Admission.Application.Common.Result;
using Admission.Domain.Common.Enums;
using Admission.User.Application.Context;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Services.Impl;

public sealed class AuthService: IAuthService
{
    private const int RefreshTokenExpirationHours = 1000;
    private const int RefreshTokenBytes = 256;
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly IUserDbContext _context;
    private readonly IJwtProvider _jwtProvider;
    public AuthService(UserManager<AdmissionUser> userManager, IJwtProvider jwtProvider, IUserDbContext context)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _context = context;
    }

    public async Task<Result<TokenPairDto>> RegisterApplicantAsync(CreateApplicantDto dto)
    {
        var defaultRole = RoleType.Applicant.ToString();
        var refreshToken = GenerateRefreshToken();
        var user = new AdmissionUser
        {
            Email = dto.Email,
            FullName = dto.FullName
        };
        SetRefreshToken(user, refreshToken);
        
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
        
        await _context.SaveChangesAsync();

        return new TokenPairDto
        {
            AccessToken = _jwtProvider.Generate(user, [defaultRole]),
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
        var user = await _context.Users
            .GetUndeleted()
            .FirstOrDefaultAsync(u => u.RefreshToken == dto.RefreshToken);
        if (user == null || user.RefreshTokenIsExpired)
        {
            return new BadRequestException("Invalid Refresh token");
        }

        return await RefreshTokens(user);
    }

    public async Task<Result> LogoutAsync(Guid userId)
    {
        var user = await _context.Users.GetByIdAsync(userId);
        if (user == null)
        {
            return new NotFoundException(nameof(AdmissionUser), userId);
        }

        if (user.RefreshToken == null)
        {
            return new BadRequestException("User has already been logged out");
        }
        
        user.RefreshToken = null;
        user.RefreshTokenExpirationTime = null;
        await _context.SaveChangesAsync();

        return Result.Success();
    }


    private async Task<TokenPairDto> RefreshTokens(AdmissionUser user)
    {
        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(user, refreshToken);
        await _context.SaveChangesAsync();
        
        return new TokenPairDto
        {
            AccessToken = _jwtProvider.Generate(user, await _userManager.GetRolesAsync(user)),
            RefreshToken = refreshToken
        };
    }

    private static void SetRefreshToken(AdmissionUser user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpirationTime = DateTime.UtcNow.AddHours(RefreshTokenExpirationHours);
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(RefreshTokenBytes));
    }
}