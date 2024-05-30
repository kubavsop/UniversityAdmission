using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.ChangeApplicantData;
using Admission.DTOs.RpcModels.UserService.ChangeManagerData;
using Admission.DTOs.RpcModels.UserService.ChangePassword;
using Admission.DTOs.RpcModels.UserService.ChangeUserRole;
using Admission.DTOs.RpcModels.UserService.DeleteUserRole;
using Admission.DTOs.RpcModels.UserService.GetApplicantData;
using Admission.DTOs.RpcModels.UserService.GetApplicants;
using Admission.DTOs.RpcModels.UserService.GetManagerData;
using Admission.DTOs.RpcModels.UserService.GetManagers;
using Admission.DTOs.RpcModels.UserService.Login;
using Admission.DTOs.RpcModels.UserService.Register;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services.Base;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Admission.AdminPanel.Services.Impl;

public sealed class RpcUserClient: BaseRpcClient, IRpcUserClient
{
    private const string QueueName = "UserRpcQueue";
    private const string ReplyQueueName = "RpcUserClientAdminPanelQueue";
    
    public RpcUserClient(IConnection connection) : base(QueueName, ReplyQueueName, connection)
    {
    }
    
    public async Task<Result<LoginResponse>> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var result = await CallAsync(loginUserRequest);
        if (result == null) return new RpcException("null response");

        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;

        return (result as LoginResponse)!;
    }
    
    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterUserRequest registerUserRequest)
    {
        var result = await CallAsync(registerUserRequest);
        if (result == null) return new RpcException("null response");

        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;
        
        return (result as RegisterResponse)!;
    }

    public async Task<Result<ManagersResponse>> GetManagersAsync(GetManagersRequest getManagersRequest)
    {
        var result = await CallAsync(getManagersRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;
        
        return (result as ManagersResponse)!;
    }

    public async Task<Result<ManagerDataResponse>> GetManagerAsync(GetManagerDataRequest getManagerDataRequest)
    {
        var result = await CallAsync(getManagerDataRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;
        
        return (result as ManagerDataResponse)!;
    }

    public async Task<Result<ApplicantsResponse>> GetApplicantsAsync(GetApplicantsRequest applicantsRequest)
    {
        var result = await CallAsync(applicantsRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;
        
        return (result as ApplicantsResponse)!;
    }

    public async Task<Result<ApplicantDataResponse>> GetApplicantAsync(GetApplicantDataRequest getApplicantDataRequest)
    {
        var result = await CallAsync(getApplicantDataRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        if (errorResult.IsFailure) return errorResult.Exception;
        
        return (result as ApplicantDataResponse)!;
    }

    public async Task<Result> DeleteManagerRoleAsync(DeleteManagerRequest deleteManagerRequest)
    {
        var result = await CallAsync(deleteManagerRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var result = await CallAsync(changePasswordRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangeManagerDataAsync(ChangeManagerDataRequest changeManagerDataRequest)
    {
        var result = await CallAsync(changeManagerDataRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> ChangeApplicantDataAsync(ChangeApplicantDataRequest changeApplicantDataRequest)
    {
        var result = await CallAsync(changeApplicantDataRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;
    }

    public async Task<Result> CreateManagerAsync(CreateManagerRequest createManagerRequest)
    {
        var result = await CallAsync(createManagerRequest);
        if (result == null) return new RpcException("null response");
        
        var errorResult = CheckError(result);
        return errorResult;    
    }
}