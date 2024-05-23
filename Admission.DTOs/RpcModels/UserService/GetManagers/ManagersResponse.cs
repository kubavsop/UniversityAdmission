namespace Admission.DTOs.RpcModels.UserService.GetManagers;

public sealed class ManagersResponse: IRpcResponse
{
    public required IEnumerable<ShortManagerDataResponse> Managers { get; init; }
}