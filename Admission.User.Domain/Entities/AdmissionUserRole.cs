using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class AdmissionUserRole: IdentityUserRole<Guid>
{
    public AdmissionUser User { get; set; }
    public AdmissionRole Role { get; set; }
} 