using Admission.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class AdmissionUserRole: IdentityUserRole<Guid>
{
    public override Guid UserId { get; set; }
    public override Guid RoleId { get; set; }
    public AdmissionUser User { get; set; } = null!;
    public AdmissionRole Role { get; set; } = null!;
} 