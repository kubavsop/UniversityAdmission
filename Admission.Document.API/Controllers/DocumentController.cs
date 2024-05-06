using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Application.Common.Exceptions;
using Admission.Document.Application.DTOs;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;
using Admission.Document.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Document.API.Controllers;

public sealed class DocumentController: BaseController
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost]
    [Authorize]
    [Route("passport")]
    public async Task<IActionResult> CreatePassport(CreatePassportDto passportDto)
    {
        var result = await _documentService.CreatePassportAsync(passportDto, UserId);
        return result.ToIActionResult();
    }

    [HttpGet]
    [Authorize]
    [Route("passport")]
    public async Task<IActionResult> GetPassport()
    {
        var result = await _documentService.GetPassportAsync(UserId);
        return result.ToIActionResult();
    }

    [HttpPut]
    [Authorize]
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
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("education")]
    public async Task<IActionResult> GetEducationDocument()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("education/{id:guid}")]
    public async Task<IActionResult> EditEducationDocument(EditEducationDocumentTypeDto educationDocumentTypeDto, Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("education/{id:guid}")]
    public async Task<IActionResult> DeleteEducationDocument(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route(("{id:guid}/scan"))]
    public async Task<IActionResult> CreateScan(Guid id, IFormFile file)
    {
        throw new NotFoundException();
    }
    
}