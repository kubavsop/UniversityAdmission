namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

public sealed class StudentAdmissionsResponse: IRpcResponse
{
    public required IEnumerable<StudentAdmissionResponse> Admissions { get; init; }
    public required PageInfoRpcModel PageInfoModel { get; init; }
}