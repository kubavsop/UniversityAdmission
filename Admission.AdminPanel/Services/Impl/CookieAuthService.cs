using System.Security.Claims;
using Admission.AdminPanel.Models;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.UserService.Login;
using Microsoft.AspNetCore.Authentication;

namespace Admission.AdminPanel.Services.Impl;

public class CookieAuthService: ICookieAuthService
{
    private readonly IRpcUserClient _userClient;

    public CookieAuthService(IRpcUserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<Result<ClaimsIdentity>> Login(LoginViewModel dto)
    {
        var response = await _userClient.LoginAsync(new LoginUserRequest
        {
            Email = dto.Email,
            Password = dto.Password
        });

        if (response.IsFailure) return response.Exception;
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, response.Value.RoleType.ToString()),
            new Claim(ClaimTypes.NameIdentifier, response.Value.UserId.ToString())
        };
        
        return new ClaimsIdentity(claims, "ApplicationCookie");
    }
}