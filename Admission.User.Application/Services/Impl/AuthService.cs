using Admission.Application.Common.DTOs.Requests;
using Admission.Application.Common.DTOs.Responses;
using Admission.Application.Common.Result;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Application.Services.Impl;

public class AuthService: IAuthService
{
    private readonly UserManager<AdmissionUser> _userManager;
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

    public Task<Result<TokenPairDto>> Login(LoginCredentialsDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<TokenPairDto>> Refresh(RefreshDto dto)
    {
        throw new NotImplementedException();
    }
}