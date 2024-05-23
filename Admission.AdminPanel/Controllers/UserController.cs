
using System.Security.Claims;
using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Constants;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.ChangeManagerData;
using Admission.DTOs.RpcModels.UserService.ChangePassword;
using Admission.DTOs.RpcModels.UserService.GetManagerData;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

[AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
public sealed class UserController: Controller
{
    private readonly IRpcUserClient _userClient;
    private readonly IMapper _mapper;

    public UserController(IRpcUserClient userClient, IMapper mapper)
    {
        _userClient = userClient;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var request = User.SetAuthRequest(new GetManagerDataRequest());
        request.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        var managerProfileResult = await _userClient.GetManagerAsync(request);
        if (managerProfileResult.IsFailure)
        {
            ModelState.AddModelError("GettingError", managerProfileResult.Exception.Message);
            return View(new ManagerProfileViewModel());
        }
        return View(_mapper.Map<ManagerProfileViewModel>(managerProfileResult.Value));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ManagerProfileViewModel profile)
    {
        var result = await _userClient.ChangeManagerDataAsync(User.SetAuthRequest(new ChangeManagerDataRequest
        {
            ManagerId = profile.ManagerId,
            Email = profile.Email,
            FullName = profile.FullName
        }));
        
        if (result.IsFailure)
        {
            ModelState.AddModelError("ChangeError", result.Exception.Message);
        }
        return View(profile);
    }
    
    
    [HttpGet]
    public IActionResult Password()
    {
        return View(new ChangePasswordViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Password(ChangePasswordViewModel changePasswordViewModel)
    {
        var result = await _userClient.ChangePasswordAsync(User.SetAuthRequest(new ChangePasswordRequest()
        {
            OldPassword = changePasswordViewModel.OldPassword,
            NewPassword = changePasswordViewModel.NewPassword
        }));
        
        if (result.IsFailure)
        {
            ModelState.AddModelError("ChangeError", result.Exception.Message);
        }
        return View(new ChangePasswordViewModel());    
    }
}