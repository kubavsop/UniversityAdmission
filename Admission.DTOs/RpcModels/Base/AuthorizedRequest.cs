using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.Base;

public abstract class AuthorizedRequest
{
    public Guid Id { get; set; }
    public RoleType Role { get; set; }
}