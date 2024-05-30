using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmission;

public sealed class GetStudentAdmissionRequest: AuthorizedRequest, IRpcRequest<StudentAdmissionResponse>
{
    public Guid StudentAdmissionId { get; init; }
}