using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.GetManagers;

public sealed class GetManagersRequest: AuthorizedRequest, IRpcRequest<ManagersResponse>;