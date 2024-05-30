using Admission.Application.Common.Result;
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

namespace Admission.AdminPanel.Services;

public interface IRpcUserClient
{
    Task<Result<LoginResponse>> LoginAsync(LoginUserRequest loginUserRequest);
    Task<Result<RegisterResponse>> RegisterAsync(RegisterUserRequest registerUserRequest);
    Task<Result<ManagersResponse>> GetManagersAsync(GetManagersRequest getManagersRequest);
    Task<Result<ManagerDataResponse>> GetManagerAsync(GetManagerDataRequest getManagerDataRequest);
    Task<Result<ApplicantsResponse>> GetApplicantsAsync(GetApplicantsRequest applicantsRequest);
    Task<Result<ApplicantDataResponse>> GetApplicantAsync(GetApplicantDataRequest getApplicantDataRequest);
    Task<Result> DeleteManagerRoleAsync(DeleteManagerRequest deleteManagerRequest);
    Task<Result> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    Task<Result> ChangeManagerDataAsync(ChangeManagerDataRequest changeManagerDataRequest);
    Task<Result> ChangeApplicantDataAsync(ChangeApplicantDataRequest changeApplicantDataRequest);
    Task<Result> CreateManagerAsync(CreateManagerRequest createManagerRequest);
}