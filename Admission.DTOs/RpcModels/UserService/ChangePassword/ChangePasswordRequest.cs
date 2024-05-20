using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.ChangePassword;

public sealed class ChangePasswordRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; init; }
}