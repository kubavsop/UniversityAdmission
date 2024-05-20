using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.Base;

public abstract class AuthorizedRequest
{
    public required Guid Id { get; init; }
    public required RoleType Role { get; init; }
}