using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Applicant;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.ChangeApplicantData;
using Admission.DTOs.RpcModels.UserService.ChangeUserRole;
using Admission.DTOs.RpcModels.UserService.GetApplicantData;
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
        var result = await _userClient.CreateManagerAsync(User.SetAuthRequest(new CreateManagerRequest
            { UserId = roleViewModel.ApplicantId, UserRole = roleViewModel.RoleToAdd }));
        
        if (result.IsFailure)
        {
            // TODO
        }
        
        return RedirectToAction("Applicants");
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Applicant([FromRoute] Guid id)
    {
        var applicantResult =
            await _userClient.GetApplicantAsync(User.SetAuthRequest(new GetApplicantDataRequest { ApplicantId = id }));
        if (applicantResult.IsFailure)
        {
            ModelState.AddModelError("GettingError", applicantResult.Exception.Message);
            return View(new ApplicantViewModel());
        }

        return View(_mapper.Map<ApplicantViewModel>(applicantResult.Value));
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Applicant(ApplicantViewModel applicantViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(applicantViewModel);
        }
        
        var changeApplicantResult = await _userClient.ChangeApplicantDataAsync(User.SetAuthRequest(
            new ChangeApplicantDataRequest
            {
                ApplicantId = applicantViewModel.ApplicantId,
                FullName = applicantViewModel.FullName,
                Email = applicantViewModel.Email,
                Birthday = applicantViewModel.Birthday,
                Gender = applicantViewModel.Gender,
                Citizenship = applicantViewModel.Citizenship,
                PhoneNumber = applicantViewModel.PhoneNumber
            }));

        if (changeApplicantResult.IsFailure)
        {
            ModelState.AddModelError("UpdatingError", changeApplicantResult.Exception.Message);
        }

        return View(applicantViewModel);
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public Task<IActionResult> Passport([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public Task<IActionResult> EducationDocuments([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}