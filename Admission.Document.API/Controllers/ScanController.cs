using Admission.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Document.API.Controllers;

public sealed class ScanController: BaseController
{
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetScan(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteScan(Guid id)
    {
        throw new NotImplementedException();
    }
}