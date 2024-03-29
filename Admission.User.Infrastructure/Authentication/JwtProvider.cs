using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Admission.Infrastructure.Common.Authentication;
using Admission.User.Application.Services;
using Admission.User.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Admission.User.Infrastructure.Authentication;

internal sealed class JwtProvider: IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string Generate(AdmissionUser user, IEnumerable<string> roles)
    {
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: GetClaims(user, roles),
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
            signingCredentials: GetSigningCredentials());
        
        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        return tokenValue;
    }
    
    
    private SigningCredentials GetSigningCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private IEnumerable<Claim> GetClaims(AdmissionUser user, IEnumerable<string> roles)
    {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}