using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.UserService.GetManagers;

public sealed class ShortManagerDataResponse
{
    public required Guid ManagerId { get; init; }
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required RoleType ManagerRole { get; init; }
}