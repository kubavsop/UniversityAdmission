using Admission.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

public sealed class GroupController: BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        throw new NotImplementedException();
    }
}