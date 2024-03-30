using Admission.API.Common;
using Admission.Application.Common.DTOs.Requests;
using Admission.Application.Common.DTOs.Responses;
using Admission.User.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admission.User.API.Controllers;

public sealed class ApplicantController: BaseController
{
    private readonly IAuthService _authService;

    public ApplicantController(IAuthService authService)
    {
        _authService = authService;
    }

    [Route("register")]
    [HttpPost]
    public Task<ActionResult<TokenPairDto>> Register(CreateApplicantDto applicantDto)
    {
        throw new NotImplementedException();
    }
    
    [Route("login")]
    [HttpPost]
    public ActionResult<TokenPairDto> Login(LoginCredentialsDto credentialsDto)
    {
        throw new NotImplementedException();
    }
    
    [Route("refresh")]
    [HttpPost]
    public Task<ActionResult<TokenPairDto>> Refresh(RefreshDto refreshDto)
    {
        throw new NotImplementedException();
    }
    
    [Route("profile")]
    [HttpGet]
    public Task<ActionResult<ApplicantDto>> GetProfile()
    {
        throw new NotImplementedException();
    }
    
    [Route("profile")]
    [HttpPut]
    public Task<IActionResult> EditProfile(EditApplicantDto editApplicantDto)
    {
        throw new NotImplementedException();
    }
    
    [Route("password")]
    [HttpPut]
    public Task<IActionResult> EditPassword(EditPasswordDto editPasswordDto)
    {
        throw new NotImplementedException();
    }
}