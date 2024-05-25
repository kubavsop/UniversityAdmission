using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.DeleteAdmissionProgram;

public sealed class DeleteAdmissionProgramRequest: AuthorizedRequest
{
    public required Guid AdmissionProgramId { get; init; }
}