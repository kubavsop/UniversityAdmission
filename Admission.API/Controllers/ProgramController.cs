using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Application.Common.Exceptions;
using Admission.Application.DTOs.Requests;
using Admission.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

[Authorize]
public sealed class ProgramController: BaseController
{
    private readonly IProgramService _programService;

    public ProgramController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram(CreateProgramDto createProgramDto)
    {
        var result = await _programService.CreateProgramAsync(createProgramDto, UserId);
        return result.ToIActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> EditPrograms(EditProgramsDto editProgramsDto)
    {
        var result = await _programService.EditProgramsAsync(editProgramsDto, UserId);
        return result.ToIActionResult();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteProgram(Guid id)
    {
        var result = await _programService.DeleteProgramAsync(id, UserId);
        return result.ToIActionResult();
    }
}