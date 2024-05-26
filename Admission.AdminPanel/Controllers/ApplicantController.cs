using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Applicant;
using Admission.AdminPanel.Services;
using Admission.Application.Common.Constants;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.AddScan;
using Admission.DTOs.RpcModels.DocumentService.ChangePassport;
using Admission.DTOs.RpcModels.DocumentService.DeleteScan;
using Admission.DTOs.RpcModels.DocumentService.DownloadScan;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;
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
    private readonly IRpcDocumentClient _documentClient;
    private readonly IMapper _mapper;

    public ApplicantController(IRpcUserClient userClient, IMapper mapper, IRpcDocumentClient documentClient)
    {
        _userClient = userClient;
        _mapper = mapper;
        _documentClient = documentClient;
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
    public async Task<IActionResult> Passport([FromRoute] Guid id)
    {
        var passport = await _documentClient.GetPassportAsync(User.SetAuthRequest(new GetPassportRequest { ApplicantId = id }));
        if (passport.IsFailure)
        {
            ModelState.AddModelError("GettingError", passport.Exception.Message);
            return View(new PassportViewModel());
        }

        return View(_mapper.Map<PassportViewModel>(passport.Value));
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> Passport(PassportViewModel passportViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();
        ModelState.Remove(nameof(passportViewModel.File));
        if (!ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
            return RedirectToAction("Index", "Home");
        }

        var changePassportResult = await _documentClient.ChangePassportAsync(User.SetAuthRequest(new ChangePassportRequest
        {
            PassportId = passportViewModel.DocumentId,
            Series = passportViewModel.Series!.Value,
            Number = passportViewModel.Number!.Value,
            PlaceOfBirth = passportViewModel.PlaceOfBirth,
            IssuedBy = passportViewModel.IssuedBy,
            DateIssued = passportViewModel.DateIssued!.Value
        }));
        
        if (changePassportResult.IsFailure)
        {
            ModelState.AddModelError("UpdatingError", changePassportResult.Exception.Message);
        }
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);

        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public Task<IActionResult> EducationDocuments([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> DeleteScan(ScanRpcModel scanRpcModel)
    {
        var result = await _documentClient.DeleteScanAsync(User.SetAuthRequest(new DeleteScanRequest { ScanId = scanRpcModel.ScanId }));
        
        var refererUrl = Request.Headers["Referer"].ToString();

        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> DownloadScan(ScanRpcModel scanRpcModel)
    {
        var downloadFileResult = await _documentClient.DownloadFileAsync(User.SetAuthRequest(new DownloadScanRequest{ScanId = scanRpcModel.ScanId}));
        
        if (downloadFileResult.IsFailure)
        {
            var refererUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl);
            }
        
            return RedirectToAction("Index", "Home");
        }

        var file = downloadFileResult.Value;
        var fileNameWithExtension = $"{file.Name}{ContentTypeMappings.ReverseTypeMappings[file.ContentType]}";
        
        return File(file.Bytes, file.ContentType, fileNameWithExtension);
    }

    [HttpPost]
    [AuthorizeRole([RoleType.Admin, RoleType.Manager, RoleType.SeniorManager])]
    public async Task<IActionResult> AddScan(DocumentViewModel scanViewModel)
    {
        var refererUrl = Request.Headers["Referer"].ToString();
        var isFileValid = !ModelState[nameof(scanViewModel.File)]?.Errors.Any();
        if (isFileValid == null || !isFileValid.Value || scanViewModel.File == null)
        {
            if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
            return RedirectToAction("Index", "Home");
        }
        
        using (var memoryStream = new MemoryStream())
        {
            await scanViewModel.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var scanRpcFullModelRequest = new ScanRpcFullModelRequest
            {
                Name = Path.GetFileNameWithoutExtension(scanViewModel.File.FileName),
                ContentType = scanViewModel.File.ContentType,
                Bytes = fileBytes
            };

            var result = await _documentClient.AddScanAsync(User.SetAuthRequest(new AddScanRequest
            {
                ScanModelResponse = scanRpcFullModelRequest,
                DocumentId = scanViewModel.DocumentId
            }));
            
            if (result.IsFailure)
            {
                ModelState.AddModelError("AddingError", result.Exception.Message);
            }
        }
        
        if (!string.IsNullOrEmpty(refererUrl)) return Redirect(refererUrl);
        return RedirectToAction("Index", "Home");    
    }
}