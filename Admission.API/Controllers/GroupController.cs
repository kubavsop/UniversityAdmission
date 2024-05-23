using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

[Authorize]
public sealed class GroupController: BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var result = await _groupService.GetGroupsAsync(UserId);
        return result.ToIActionResult();
    }
}