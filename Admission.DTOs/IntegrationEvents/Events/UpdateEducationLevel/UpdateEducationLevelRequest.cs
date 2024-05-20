using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.IntegrationEvents.Events.UpdateEducationLevel;

public sealed class UpdateEducationLevelRequest: AuthorizedRequest, IIntegrationEvent;