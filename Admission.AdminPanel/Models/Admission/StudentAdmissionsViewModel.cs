using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

namespace Admission.AdminPanel.Models.Admission;

public sealed class StudentAdmissionsViewModel: IMapFrom<StudentAdmissionsResponse>
{
    public required IEnumerable<StudentAdmissionResponse> Admissions { get; init; }
    public required PageInfoRpcModel PageInfoModel { get; init; }
    public GetStudentAdmissionsRequest StudentAdmissionsRequest { get; set; }
}