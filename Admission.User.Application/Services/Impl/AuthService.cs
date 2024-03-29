using Admission.Application.Common.DTOs.Requests;
using Admission.Application.Common.DTOs.Responses;
using Admission.Application.Common.Result;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Application.Services.Impl;

public class AuthService: IAuthService
{
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly RoleManager<AdmissionRole> _role;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(UserManager<AdmissionUser> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }


    public Task<Result<TokenPairDto>> RegisterApplicant(CreateApplicantDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TokenPairDto>> Login(LoginCredentialsDto dto)
    {
        var user = new AdmissionUser
        {
            Email = "my@gmail.com",
            FullName = "MyName"
        };
        await _userManager.CreateAsync(user, "12345");
        
        
        
        await _userManager.AddToRoleAsync(user, "Admin");
        Console.WriteLine(await _userManager.GetRolesAsync(user));
        
        
        
        
        return new TokenPairDto {AccessToken = "", RefreshToken = ""};
    }

    public Task<Result<TokenPairDto>> Refresh(RefreshDto dto)
    {
        throw new NotImplementedException();
    }
}