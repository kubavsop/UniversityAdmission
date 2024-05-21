using System.Security.Claims;
using Admission.AdminPanel.Models;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.UserService.Login;
using Admission.DTOs.RpcModels.UserService.Register;

namespace Admission.AdminPanel.Services.Impl;

public class CookieAuthService: ICookieAuthService
{
    private readonly IRpcUserClient _userClient;

    public CookieAuthService(IRpcUserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<Result<ClaimsIdentity>> Login(LoginViewModel loginViewModel)
    {
        var response = await _userClient.LoginAsync(new LoginUserRequest
        {
            Email = loginViewModel.Email,
            Password = loginViewModel.Password
        });

        if (response.IsFailure) return response.Exception;
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, response.Value.RoleType.ToString()),
            new Claim(ClaimTypes.NameIdentifier, response.Value.UserId.ToString())
        };
        
        return new ClaimsIdentity(claims, "ApplicationCookie");
    }

    public async Task<Result<ClaimsIdentity>> Register(RegisterViewModel registerViewModel)
    {
        var response = await _userClient.RegisterAsync(new RegisterUserRequest
        {
            FullName = registerViewModel.FullName,
            Email = registerViewModel.Email,
            Password = registerViewModel.Password
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