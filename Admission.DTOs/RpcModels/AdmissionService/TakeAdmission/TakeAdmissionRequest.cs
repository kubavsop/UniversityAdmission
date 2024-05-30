using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.TakeAdmission;

public sealed class TakeAdmissionRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid StudentAdmissionId { get; init; }
    public required Guid ManagerId { get; init; }
}