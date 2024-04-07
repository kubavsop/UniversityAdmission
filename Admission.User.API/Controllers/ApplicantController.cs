using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.User.API.Controllers;

public sealed class ApplicantController: BaseController
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public ApplicantController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
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
    
    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync(UserId);
        return result.ToIActionResult();
    }
    
    [HttpGet]
    [Route("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _userService.GetApplicantProfileAsync(UserId);
        return result.ToIActionResult();
    }
    
    [HttpPut]
    [Route("profile")]
    [Authorize]
    public async Task<IActionResult> EditProfile(EditApplicantDto editApplicantDto)
    {
        var result = await _userService.EditApplicantProfileAsync(editApplicantDto, UserId);
        return result.ToIActionResult();
    }
    

    [HttpPut]
    [Route("password")]
    [Authorize]
    public async Task<IActionResult> EditPassword(EditPasswordDto editPasswordDto)
    {
        var result = await _userService.EditPasswordAsync(editPasswordDto , UserId);
        return result.ToIActionResult();
    }
}