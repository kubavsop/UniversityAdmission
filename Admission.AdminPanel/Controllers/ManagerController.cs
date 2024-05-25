using System.Security.Claims;
using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Manager;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.UserService.ChangeManagerData;
using Admission.DTOs.RpcModels.UserService.ChangePassword;
using Admission.DTOs.RpcModels.UserService.DeleteUserRole;
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
        return View(new ManagerProfileViewModel
        {
            FullName = managerProfileResult.Value.FullName,
            Email = managerProfileResult.Value.Email,
            FacultyId = managerProfileResult.Value.Faculty?.Id,
            FacultyName = managerProfileResult.Value.Faculty?.Name,
            ManagerId = managerProfileResult.Value.ManagerId
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Profile(ManagerProfileViewModel profile)
    {
        if (!ModelState.IsValid) return View(profile);
        
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
        if (!ModelState.IsValid) return View(changePasswordViewModel);
        
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
        
        if (!profile.IsFailure) return View(new ManagerProfileViewModel
        {
            FullName = profile.Value.FullName,
            Email = profile.Value.Email,
            FacultyId = profile.Value.Faculty?.Id,
            FacultyName = profile.Value.Faculty?.Name,
            ManagerId = profile.Value.ManagerId
        });
        
        ModelState.AddModelError("GettingError", profile.Exception.Message);
        return View(new ManagerProfileViewModel());
    }

    [HttpPost]
    [AuthorizeRole(RoleType.Admin)]
    public async Task<IActionResult> Manager(ManagerProfileViewModel profile)
    {
        ViewData["Faculties"] = (await _dictionaryMvcClient.GetFaculties(User.SetAuthRequest(new GetFacultiesRequest()))).Value.Faculties;
        if (!ModelState.IsValid) return View(profile);
        
        var result = await _userClient.ChangeManagerDataAsync(User.SetAuthRequest(new ChangeManagerDataRequest
        {
            ManagerId = profile.ManagerId,
            Email = profile.Email,
            FullName = profile.FullName,
            FacultyId = profile.FacultyId,
            FacultyName = profile.FacultyName
        }));
        
        if (result.IsFailure)
        {
            ModelState.AddModelError("ChangeError", result.Exception.Message);
        }
        
        return View(profile);
    }
    
    [AuthorizeRole(RoleType.Admin)]
    public async Task<IActionResult> DeleteManager([FromRoute] Guid id)
    {
        var result = await _userClient.DeleteManagerRoleAsync(User.SetAuthRequest(new DeleteManagerRequest { UserId = id }));
        
        if (result.IsFailure)
        {
            // TODO
        }
        
        return RedirectToAction("Managers");
    }
}