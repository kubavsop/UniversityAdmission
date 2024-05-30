using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.ChangeProgramPriorities;

public sealed class ChangeProgramsPrioritiesRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid StudentAdmissionId { get; init; }
    public required IEnumerable<ChangeProgramModel> Programs { get; init; }
}