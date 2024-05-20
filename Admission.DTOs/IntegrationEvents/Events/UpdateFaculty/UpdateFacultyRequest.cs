using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.IntegrationEvents.Events.UpdateFaculty;

public sealed class UpdateFacultyRequest: AuthorizedRequest, IIntegrationEvent;