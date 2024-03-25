using Admission.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class Role: IdentityRole<Guid>
{
    public RoleType Type { get; set; }
}