using Admission.API.Common;
using Admission.Application.Common.Exceptions;
using Admission.Document.Application.DTOs;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Document.API.Controllers;

public sealed class DocumentController: BaseController
{
    [HttpPost]
    [Route("passport")]
    public async Task<IActionResult> CreatePassport(CreatePassportDto passportDto)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("passport")]
    public async Task<IActionResult> GetPassport()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("passport")]
    public async Task<IActionResult> EditPassport(EditPassportDto passportDto)
    {
        throw new NotImplementedException();
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