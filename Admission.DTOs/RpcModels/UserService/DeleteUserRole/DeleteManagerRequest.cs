using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.DeleteUserRole;

public class DeleteManagerRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid UserId { get; init; }
}