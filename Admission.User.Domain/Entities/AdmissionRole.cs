using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class AdmissionRole: IdentityRole<Guid>, IBaseEntity
{
    public RoleType Type { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? DeleteTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
}