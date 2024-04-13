using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Requests;
using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Dictionary.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Dictionary.API.Controllers;


public sealed class DictionaryController: BaseController
{
    private readonly IDictionaryService _dictionaryService;
    private readonly IImporterService _importerService;

    public DictionaryController(IDictionaryService dictionaryService, IImporterService importerService)
    {
        _dictionaryService = dictionaryService;
        _importerService = importerService;
    }

    [HttpGet]
    [Route("education-levels")]
    public async Task<IActionResult> GetEducationLevels()
    {
        var result = await _dictionaryService.GetEducationLevelsAsync();
        return result.ToIActionResult();
    }
    
    [HttpGet]
    [Route("document-types")]
    public async Task<IActionResult> GetDocumentTypes()
    {
        var result = await _dictionaryService.GetDocumentTypesAsync();
        return result.ToIActionResult();
    }
    
    [HttpGet]
    [Route("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        var result = await _dictionaryService.GetFacultiesAsync();
        return result.ToIActionResult();
    }
    
    [HttpGet]
    [Route("programs")]
    public async Task<IActionResult> GetEducationPrograms([FromQuery] ProgramSearchParameters parameters)
    {
        var result = await _dictionaryService.GetEducationProgramsAsync(parameters);
        return result.ToIActionResult();
    }
    
    [HttpPost]
    [Route("Test")]
    public async Task TestUpdate()
    {
        await _importerService.TestUpdate();
    }
}