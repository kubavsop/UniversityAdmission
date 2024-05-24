using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Applicant;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.ChangeUserRole;
using Admission.DTOs.RpcModels.UserService.GetApplicants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

public sealed class ApplicantController: Controller
{
    private readonly IRpcUserClient _userClient;
    private readonly IMapper _mapper;

    public ApplicantController(IRpcUserClient userClient, IMapper mapper)
    {
        _userClient = userClient;
        _mapper = mapper;
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Applicants()
    {
        var result = await _userClient.GetApplicantsAsync(User.SetAuthRequest(new GetApplicantsRequest()));

        if (result.IsFailure)
        {
            // TODO
            return View(new ApplicantsViewModel{Applicants = []});
        }

        return View(_mapper.Map<ApplicantsViewModel>(result.Value));
    }

    [HttpPost]
    [AuthorizeRole(RoleType.Admin)]
    public async Task<IActionResult> Role(AddRoleViewModel roleViewModel)
    {
        var result = await _userClient.AddUserRoleAsync(User.SetAuthRequest(new AddUserRoleRequest
            { UserId = roleViewModel.ApplicantId, UserRole = roleViewModel.RoleToAdd }));
        
        if (result.IsFailure)
        {
            // TODO
        }
        
        return RedirectToAction("Applicants");
    }
}