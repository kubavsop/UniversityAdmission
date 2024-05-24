using System.Security.Claims;
using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models;
using Admission.AdminPanel.Models.Manager;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.UserService.ChangeManagerData;
using Admission.DTOs.RpcModels.UserService.ChangePassword;
using Admission.DTOs.RpcModels.UserService.GetManagerData;
using Admission.DTOs.RpcModels.UserService.GetManagers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

public sealed class ManagerController: Controller
{
    private readonly IRpcUserClient _userClient;
    private readonly IRpcDictionaryMvcClient _dictionaryMvcClient;
    private readonly IMapper _mapper;

    public ManagerController(IRpcUserClient userClient, IMapper mapper, IRpcDictionaryMvcClient dictionaryMvcClient)
    {
        _userClient = userClient;
        _mapper = mapper;
        _dictionaryMvcClient = dictionaryMvcClient;
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Profile()
    {
        var request = User.SetAuthRequest(new GetManagerDataRequest{ UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) });
        
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
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
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
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]

    public IActionResult Password()
    {
        return View(new ChangePasswordViewModel());
    }
    
    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Password(ChangePasswordViewModel changePasswordViewModel)
    {
        var result = await _userClient.ChangePasswordAsync(User.SetAuthRequest(new ChangePasswordRequest
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
    
    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.SeniorManager])]
    public async Task<IActionResult> Managers()
    {
        var result = await _userClient.GetManagersAsync(User.SetAuthRequest(new GetManagersRequest()));

        if (result.IsFailure)
        {
            // TODO
            return View(new ManagersViewModel{Managers = []});
        }

        return View(_mapper.Map<ManagersViewModel>(result.Value));
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.SeniorManager])]
    public async Task<IActionResult> Manager([FromRoute] Guid id)
    {
        ViewData["Faculties"] = (await _dictionaryMvcClient.GetFaculties(User.SetAuthRequest(new GetFacultiesRequest()))).Value.Faculties;
        var profile = await _userClient.GetManagerAsync(User.SetAuthRequest(new GetManagerDataRequest{ UserId = id }));
        
        if (!profile.IsFailure) return View(_mapper.Map<ManagerProfileViewModel>(profile.Value));
        
        ModelState.AddModelError("GettingError", profile.Exception.Message);
        return View(new ManagerProfileViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Manager(ManagerProfileViewModel profile)
    {
        var result = await _userClient.ChangeManagerDataAsync(User.SetAuthRequest(new ChangeManagerDataRequest
        {
            ManagerId = profile.ManagerId,
            Email = profile.Email,
            FullName = profile.FullName,
            FacultyId = profile.Faculty?.Id,
            FacultyName = profile.Faculty?.Name
        }));
        
        if (result.IsFailure)
        {
            ModelState.AddModelError("ChangeError", result.Exception.Message);
        }
        
        return View(profile);
    }
}