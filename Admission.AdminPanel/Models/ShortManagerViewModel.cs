using Admission.Application.Common.Mapping;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.GetManagers;

namespace Admission.AdminPanel.Models;

public sealed class ShortManagerViewModel: IMapFrom<ShortManagerDataResponse>
{
    public required Guid ManagerId { get; init; }
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required RoleType ManagerRole { get; init; }
}