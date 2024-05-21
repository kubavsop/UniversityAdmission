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
    
    public RpcUserClient(IOptions<RpcClientQueueNameOptions> replyQueueName, IConnection connection) : base(QueueName, replyQueueName.Value.Name, connection)
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

    public Task<Result<ManagersResponse>> GetManagersAsync(GetManagersRequest getManagersRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ManagerDataResponse>> GetManagerAsync(GetManagerDataRequest getManagerDataRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ApplicantsResponse>> GetApplicantsAsync(GetApplicantsRequest applicantsRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ApplicantDataResponse>> GetApplicantAsync(GetApplicantDataRequest getApplicantDataRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserRoleAsync(DeleteUserRoleRequest deleteUserRoleRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangeManagerDataAsync(ChangeManagerDataRequest changeManagerDataRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangeApplicantDataAsync(ChangeApplicantDataRequest changeApplicantDataRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AddUserRoleAsync(AddUserRoleRequest addUserRoleRequest)
    {
        throw new NotImplementedException();
    }

    private Result CheckError(IRpcResponse rpcResponse)
    {
        if (rpcResponse is RpcErrorResponse error) return new RpcException(error.Message);
        return Result.Success();
    }
}