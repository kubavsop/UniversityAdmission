using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Admission.AdminPanel.Attributes;
using Admission.AdminPanel.Constants;
using Admission.AdminPanel.Extensions;
using Admission.AdminPanel.Models.Dictionary;
using Admission.AdminPanel.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateDictionary;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;
using Admission.DTOs.RpcModels.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

[AuthorizeRole(RoleType.Admin)]
public sealed class DictionaryController: Controller
{
    private readonly IRpcDictionaryMvcClient _client;
    private readonly IIntegrationEventPublisher _publisher;
    private readonly IMapper _mapper;

    public DictionaryController(IRpcDictionaryMvcClient client, IMapper mapper, IIntegrationEventPublisher publisher)
    {
        _client = client;
        _mapper = mapper;
        _publisher = publisher;
    }

    [HttpGet]
    public async Task<IActionResult> Dictionaries()
    {
        var statuses = await _client.GetUpdateStatusesAsync(User.SetAuthRequest(new GetUpdateStatusRequest()));
        
        return View(_mapper.Map<UpdateDictionaryViewModel>(statuses.Value));
    }

    [HttpPost]
    public IActionResult Update(UpdateDictionaryViewModel viewModel)
    {
        _publisher.Publish(new UpdateDictionaryIntegrationEvent
        {
            Id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
            Role = Enum.Parse<RoleType>(User.FindFirst(ClaimTypes.Role)!.Value),
            Options = viewModel.Options
        }, RoutingKeys.UpdateDictionaryRoutingKey);
        
        return RedirectToAction("Dictionaries");
    }
}