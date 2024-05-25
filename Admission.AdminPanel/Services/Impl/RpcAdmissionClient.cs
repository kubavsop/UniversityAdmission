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
using Admission.RabbitMQ.Services.Base;
using RabbitMQ.Client;

namespace Admission.AdminPanel.Services.Impl;

public sealed class RpcAdmissionClient: BaseRpcClient, IRpcAdmissionClient
{
    private const string QueueName = "AdmissionRpcQueue";
    private const string ReplyQueueName = "RpcAdmissionClientAdminPanelQueue";

    public RpcAdmissionClient(IConnection connection) : base(QueueName, ReplyQueueName, connection)
    {
    }

    public Task<Result> ChangeProgramsPrioritiesAsync(ChangeProgramsPrioritiesRequest programsPrioritiesRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangeStudentAdmissionStatusAsync(ChangeStudentAdmissionStatusRequest changeStudentAdmissionStatusRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CreateAdmissionGroupAsync(CreateAdmissionGroupRequest admissionGroupRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAdmissionProgramAsync(DeleteAdmissionProgramRequest deleteAdmissionProgramRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RefuseAdmissionAsync(RefuseAdmissionRequest refuseAdmissionRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> TakeAdmissionAsync(TakeAdmissionRequest takeAdmissionRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AdmissionGroupsResponse>> GetAdmissionGroupsAsync(GetAdmissionGroupsRequest getAdmissionGroupsRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AdmissionProgramsResponse>> GetAdmissionProgramsAsync(GetAdmissionProgramsRequest programsRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<StudentAdmissionsResponse>> GetStudentAdmissionsRequestAsync(GetStudentAdmissionsRequest admissionsRequest)
    {
        throw new NotImplementedException();
    }
}