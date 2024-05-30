using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.AdmissionService.ChangeProgramPriorities;
using Admission.DTOs.RpcModels.AdmissionService.ChangeStudentAdmissionStatus;
using Admission.DTOs.RpcModels.AdmissionService.CreateAdmissionGroup;
using Admission.DTOs.RpcModels.AdmissionService.DeleteAdmissionProgram;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;
using Admission.DTOs.RpcModels.AdmissionService.RefuseAdmission;
using Admission.DTOs.RpcModels.AdmissionService.TakeAdmission;

namespace Admission.AdminPanel.Services;

public interface IRpcAdmissionClient
{
    Task<Result> ChangeProgramsPrioritiesAsync(ChangeProgramsPrioritiesRequest programsPrioritiesRequest);
    Task<Result> ChangeStudentAdmissionStatusAsync(ChangeStudentAdmissionStatusRequest changeStudentAdmissionStatusRequest);
    Task<Result> CreateAdmissionGroupAsync(CreateAdmissionGroupRequest admissionGroupRequest);
    Task<Result> DeleteAdmissionProgramAsync(DeleteAdmissionProgramRequest deleteAdmissionProgramRequest);
    Task<Result> RefuseAdmissionAsync(RefuseAdmissionRequest refuseAdmissionRequest);
    Task<Result> TakeAdmissionAsync(TakeAdmissionRequest takeAdmissionRequest);
    Task<Result<AdmissionGroupsResponse>> GetAdmissionGroupsAsync(GetAdmissionGroupsRequest getAdmissionGroupsRequest);
    Task<Result<AdmissionProgramsResponse>> GetAdmissionProgramsAsync(GetAdmissionProgramsRequest programsRequest);
    Task<Result<StudentAdmissionsResponse>> GetStudentAdmissionsRequestAsync(GetStudentAdmissionsRequest admissionsRequest);
}