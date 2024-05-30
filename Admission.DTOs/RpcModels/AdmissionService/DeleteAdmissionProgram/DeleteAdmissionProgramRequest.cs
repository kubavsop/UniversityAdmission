using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.DeleteAdmissionProgram;

public sealed class DeleteAdmissionProgramRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid AdmissionProgramId { get; init; }
}