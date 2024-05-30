using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.Enums;

namespace Admission.DTOs.IntegrationEvents.Events.UpdateDictionary;

public sealed class UpdateDictionaryIntegrationEvent : AuthorizedRequest, IIntegrationEvent
{
    public UpdateOptions Options { get; init; }
}