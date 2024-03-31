using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Application.Common.DTOs.Requests;
using Admission.User.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.User.API.Controllers;

public sealed class ApplicantController: BaseController
{
    private readonly IAuthService _authService;

    public ApplicantController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(CreateApplicantDto applicantDto)
    {
        var result = await _authService.RegisterApplicantAsync(applicantDto);
        return result.ToIActionResult();
    }
    

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginCredentialsDto credentialsDto)
    {
        var result = await _authService.LoginAsync(credentialsDto);
        return result.ToIActionResult();
    }


    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(RefreshDto refreshDto)
    {
        var result = await _authService.RefreshAsync(refreshDto);
        return result.ToIActionResult();
    }
    
    
    [HttpGet]
    [Route("profile")]
    [Authorize]
    public Task<IActionResult> GetProfile()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [Route("profile")]
    public Task<IActionResult> EditProfile(EditApplicantDto editApplicantDto)
    {
        throw new NotImplementedException();
    }
    

    [HttpPut]
    [Route("password")]
    public Task<IActionResult> EditPassword(EditPasswordDto editPasswordDto)
    {
        throw new NotImplementedException();
    }
}