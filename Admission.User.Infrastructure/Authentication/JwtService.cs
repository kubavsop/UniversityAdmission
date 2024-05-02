using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.JWT.Options;
using Admission.User.Application.Models;
using Admission.User.Application.Services;
using Admission.User.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Admission.User.Infrastructure.Authentication;

public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string Generate(AdmissionUser user, IEnumerable<string> roles, Guid tokenId)
    {
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: GetClaims(user, tokenId, roles),
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
            signingCredentials: GetSigningCredentials());

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return tokenValue;
    }

    public Result<TokenIdentifiers> GetIdentifiersFromToken(string token)
    {
        var principalResult = GetPrincipalFromExpiredToken(token);
        if (principalResult.IsFailure)
        {
            return principalResult.Exception;
        }

        var principial = principalResult.Value;

        var userId = principial.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var tokenId = principial.FindFirst(JwtRegisteredClaimNames.Jti)?.Value!;

        return new TokenIdentifiers
        {
            TokenId = Guid.Parse(tokenId),
            UserId = Guid.Parse(userId)
        };
    }


    private Result<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateLifetime = false
        };

        ClaimsPrincipal principal;

        try
        {
            principal =
                new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenInvalidTypeException();
        }
        catch (SecurityTokenInvalidTypeException e)
        {
            throw;
        }
        catch (Exception e)
        {
            return new BadRequestException("Invalid access token");
        }
        
        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var securityKey = GetSymmetricSecurityKey();
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private IEnumerable<Claim> GetClaims(AdmissionUser user, Guid tokenId, IEnumerable<string> roles)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
        ];

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }


    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
    }
}