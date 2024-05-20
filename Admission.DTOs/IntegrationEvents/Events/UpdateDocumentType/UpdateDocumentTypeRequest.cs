using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.IntegrationEvents.Events.UpdateDocumentType;

public sealed class UpdateDocumentTypeRequest: AuthorizedRequest, IIntegrationEvent;