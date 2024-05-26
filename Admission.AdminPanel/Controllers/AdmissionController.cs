using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Admission;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.AdmissionService;
using Admission.DTOs.RpcModels.AdmissionService.ChangeProgramPriorities;
using Admission.DTOs.RpcModels.AdmissionService.ChangeStudentAdmissionStatus;
using Admission.DTOs.RpcModels.AdmissionService.CreateAdmissionGroup;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;
using Admission.DTOs.RpcModels.AdmissionService.RefuseAdmission;
using Admission.DTOs.RpcModels.AdmissionService.TakeAdmission;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.UserService.GetManagers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

public sealed class AdmissionController: Controller
{
    private readonly IRpcDictionaryMvcClient _dictionaryMvcClient;
    private readonly IRpcAdmissionClient _admissionClient;
    private readonly IRpcUserClient _userClient;
    private readonly IMapper _mapper;

    public AdmissionController(IRpcAdmissionClient admissionClient, IMapper mapper, IRpcUserClient userClient, IRpcDictionaryMvcClient dictionaryMvcClient)
    {
        _admissionClient = admissionClient;
        _mapper = mapper;
        _userClient = userClient;
        _dictionaryMvcClient = dictionaryMvcClient;
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Groups()
    {
        var result = await _admissionClient.GetAdmissionGroupsAsync(User.SetAuthRequest(new GetAdmissionGroupsRequest()));
        return View(_mapper.Map<AdmissionGroupsViewModel>(result.Value));
    }
    
    
    [AuthorizeRole(RoleType.Admin)]
    [HttpPost]
    public async Task<IActionResult> Group()
    {
        await _admissionClient.CreateAdmissionGroupAsync(User.SetAuthRequest(new CreateAdmissionGroupRequest()));
        return RedirectToAction("Groups");
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> StudentAdmissions([FromQuery] GetStudentAdmissionsRequest studentAdmissionsRequest)
    {
        ViewData["Managers"] = (await _userClient.GetManagersAsync(User.SetAuthRequest(new GetManagersRequest()))).Value.Managers;
        ViewData["Faculties"] =
            (await _dictionaryMvcClient.GetFacultiesAsync(User.SetAuthRequest(new GetFacultiesRequest()))).Value
            .Faculties;
        
        var result =
            await _admissionClient.GetStudentAdmissionsRequestAsync(User.SetAuthRequest(studentAdmissionsRequest));

        var viewModel = _mapper.Map<StudentAdmissionsViewModel>(result.Value);
        viewModel.StudentAdmissionsRequest = studentAdmissionsRequest;
        
        return View(viewModel);
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> TakeAdmission(TakeAdmissionViewModel takeAdmissionViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();
        
        var result = await _admissionClient.TakeAdmissionAsync(User.SetAuthRequest(new TakeAdmissionRequest
        {
            ManagerId = takeAdmissionViewModel.ManagerId, StudentAdmissionId = takeAdmissionViewModel.AdmissionId
        }));
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> RefuseAdmission(RefuseAdmissionViewModel refuseAdmissionViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();
        
        var result = await _admissionClient.RefuseAdmissionAsync(User.SetAuthRequest(new RefuseAdmissionRequest
        {
            ManagerId = refuseAdmissionViewModel.ManagerId, StudentAdmissionId = refuseAdmissionViewModel.AdmissionId
        }));
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Status(ChangeStatusViewModel changeStatusViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();

        var result = await _admissionClient.ChangeStudentAdmissionStatusAsync(User.SetAuthRequest(
            new ChangeStudentAdmissionStatusRequest
                { Status = changeStatusViewModel.Status, StudentAdmissionId = changeStatusViewModel.AdmissionId }));
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Programs([FromRoute] Guid id)
    {
        var programResult = await _admissionClient.GetAdmissionProgramsAsync(User.SetAuthRequest(
            new GetAdmissionProgramsRequest
            {
                StudentAdmissionId = id
            }));
        var viewModel = _mapper.Map<ProgramViewModel>(programResult.Value);
        viewModel.StudentAdmissionId = id;
        return View(viewModel);
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Programs(ProgramViewModel programViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();
        var programs = programViewModel.Programs.Select(p => new ChangeProgramModel
        {
            Id = p.Id,
            Priority = p.Priority
        }).ToList();
            
        var result = await _admissionClient.ChangeProgramsPrioritiesAsync(User.SetAuthRequest(
            new ChangeProgramsPrioritiesRequest
            {
                StudentAdmissionId = programViewModel.StudentAdmissionId,
                Programs = programs
            }));
        
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");
    }
}