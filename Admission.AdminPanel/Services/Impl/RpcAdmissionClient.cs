using Admission.Application.Common.Exceptions;
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

    public async Task<Result> ChangeProgramsPrioritiesAsync(ChangeProgramsPrioritiesRequest programsPrioritiesRequest)
    {
        var result = await CallAsync(programsPrioritiesRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangeStudentAdmissionStatusAsync(ChangeStudentAdmissionStatusRequest changeStudentAdmissionStatusRequest)
    {
        var result = await CallAsync(changeStudentAdmissionStatusRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;    }

    public async Task<Result> CreateAdmissionGroupAsync(CreateAdmissionGroupRequest admissionGroupRequest)
    {
        var result = await CallAsync(admissionGroupRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> DeleteAdmissionProgramAsync(DeleteAdmissionProgramRequest deleteAdmissionProgramRequest)
    {
        var result = await CallAsync(deleteAdmissionProgramRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> RefuseAdmissionAsync(RefuseAdmissionRequest refuseAdmissionRequest)
    {
        var result = await CallAsync(refuseAdmissionRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> TakeAdmissionAsync(TakeAdmissionRequest takeAdmissionRequest)
    {
        var result = await CallAsync(takeAdmissionRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result<AdmissionGroupsResponse>> GetAdmissionGroupsAsync(GetAdmissionGroupsRequest getAdmissionGroupsRequest)
    {
        var result = await CallAsync(getAdmissionGroupsRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as AdmissionGroupsResponse)!;
    }

    public async Task<Result<AdmissionProgramsResponse>> GetAdmissionProgramsAsync(GetAdmissionProgramsRequest programsRequest)
    {
        var result = await CallAsync(programsRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as AdmissionProgramsResponse)!;
    }

    public async Task<Result<StudentAdmissionsResponse>> GetStudentAdmissionsRequestAsync(GetStudentAdmissionsRequest admissionsRequest)
    {
        var result = await CallAsync(admissionsRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;  
        
        return (result as StudentAdmissionsResponse)!;
    }
}