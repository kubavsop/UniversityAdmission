using Admission.API.Common;
using Admission.Dictionary.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Dictionary.API.Controllers;

[Authorize]
public sealed class DictionaryController: BaseController
{
    [HttpGet]
    [Route("education-levels")]
    public async Task<IActionResult> GetEducationLevels()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("document-types")]
    public async Task<IActionResult> GetDocumentTypes()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("programs")]
    public async Task<IActionResult> GetEducationPrograms([FromQuery] ProgramSearchParameters parameters)
    {
        throw new NotImplementedException();
    }
}