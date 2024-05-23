using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.UserService.Register;

public sealed class RegisterResponse: IRpcResponse
{
    public required Guid UserId { get; init; }
    public required RoleType RoleType { get; init; }
}