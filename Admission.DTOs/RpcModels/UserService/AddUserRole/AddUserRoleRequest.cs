using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.ChangeUserRole;

public sealed class AddUserRoleRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid UserId { get; init; }
    public required RoleType UserRole { get; init; }
}