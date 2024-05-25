using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;

public sealed class GetAdmissionPrograms: AuthorizedRequest, IRpcRequest<AdmissionProgramsResponse>
{
    public required Guid StudentAdmissionId { get; init; }
}