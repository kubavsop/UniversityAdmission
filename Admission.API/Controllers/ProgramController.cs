using Admission.API.Common;
using Admission.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

[Authorize]
public sealed class ProgramController: BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateProgram(CreateProgramDto createProgramDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<IActionResult> EditPrograms(IEnumerable<EditProgramDto> editProgramDtos)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteProgram(Guid id)
    {
        throw new NotImplementedException();
    }
}