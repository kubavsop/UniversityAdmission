using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.ChangeStudentAdmissionStatus;

public sealed class ChangeStudentAdmissionStatusRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid StudentAdmissionId { get; init; }
    public required AdmissionStatus Status;
}