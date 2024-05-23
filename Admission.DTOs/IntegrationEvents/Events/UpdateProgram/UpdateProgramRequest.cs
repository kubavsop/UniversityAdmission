using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.IntegrationEvents.Events.UpdateProgram;

public sealed class UpdateProgramRequest: AuthorizedRequest, IIntegrationEvent;