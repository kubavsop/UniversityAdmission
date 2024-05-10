using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Document.API.Controllers;

[Authorize]
public sealed class DocumentController: BaseController
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost]
    [Route("passport")]
    public async Task<IActionResult> CreatePassport(CreatePassportDto passportDto)
    {
        var result = await _documentService.CreatePassportAsync(passportDto, UserId);
        return result.ToIActionResult();
    }

    [HttpGet]
    [Route("passport")]
    public async Task<IActionResult> GetPassport()
    {
        var result = await _documentService.GetPassportAsync(UserId);
        return result.ToIActionResult();
    }

    [HttpPut]
    [Route("passport")]
    public async Task<IActionResult> EditPassport(EditPassportDto passportDto)
    {
        var result = await _documentService.EditPassportAsync(passportDto, UserId);
        return result.ToIActionResult();
    }
    
    [HttpPost]
    [Route("education")]
    public async Task<IActionResult> CreateEducationDocument(CreateEducationDocumentDto createEducationDocumentDto)
    {
        var result = await _documentService.CreateEducationDocumentAsync(createEducationDocumentDto, UserId);
        return result.ToIActionResult();
    }

    [HttpGet]
    [Route("education")]
    public async Task<IActionResult> GetEducationDocument()
    {
        var result = await _documentService.GetEducationDocumentAsync(UserId);
        return result.ToIActionResult();
    }

    [HttpPut]
    [Route("education/{id:guid}")]
    public async Task<IActionResult> EditEducationDocument(EditEducationDocumentDto educationDocumentDto, Guid id)
    {
        var result = await _documentService.EditEducationDocumentAsync(educationDocumentDto, id, UserId);
        return result.ToIActionResult();
    }

    [HttpDelete]
    [Route("education/{id:guid}")]
    public async Task<IActionResult> DeleteEducationDocument(Guid id)
    {
        var result = await _documentService.DeleteEducationDocumentAsync(id, UserId);
        return result.ToIActionResult();
    }

    [HttpPost]
    [Route(("{id:guid}/scan"))]
    public async Task<IActionResult> CreateScan(Guid id, CreateScanDto fileDto)
    {
        var result = await _documentService.AddScan(UserId, id, fileDto);
        return result.ToIActionResult();
    }
}