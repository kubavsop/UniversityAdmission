using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.RefuseAdmission;

public sealed class RefuseAdmissionRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid StudentAdmissionId { get; init; }
    public required Guid ManagerId { get; init; }
}