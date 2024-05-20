using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.GetManagerData;

public sealed class GetManagerDataRequest: AuthorizedRequest, IRpcRequest<ManagerDataResponse>
{
    public Guid UserId { get; init; }
}