using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.UserService.Login;

public sealed class LoginResponse: IRpcResponse
{
    public required Guid UserId { get; init; }
    public required RoleType RoleType { get; init; }
}