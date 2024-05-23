using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.GetManagers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

[AuthorizeRole(RoleType.Admin)]
public class ManagerController: Controller
{
    private readonly IRpcUserClient _userClient;
    private readonly IMapper _mapper;

    public ManagerController(IRpcUserClient userClient, IMapper mapper)
    {
        _userClient = userClient;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Managers()
    {
        var result = await _userClient.GetManagersAsync(User.SetAuthRequest(new GetManagersRequest()));

        if (result.IsFailure)
        {
            ModelState.AddModelError("GettingError", result.Exception.Message);
            return View(new ManagersViewModel{Managers = []});
        }

        return View(_mapper.Map<ManagersViewModel>(result.Value));
    }

    [HttpGet]
    public async Task<IActionResult> Manager()
    {
        throw new NotImplementedException();
    }
}