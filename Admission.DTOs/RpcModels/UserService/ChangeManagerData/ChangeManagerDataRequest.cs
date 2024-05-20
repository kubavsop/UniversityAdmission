using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.ChangeManagerData;

public sealed class ChangeManagerDataRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid ManagerId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public string? FacultyName { get; set; }
    public Guid? FacultyId { get; set; }
}