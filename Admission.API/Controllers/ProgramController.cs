using Admission.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;


public sealed class ProgramController: BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateProgram()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<IActionResult> EditPrograms()
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