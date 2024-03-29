using System.Reflection.Metadata;
using Admission.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class AdmissionUser: IdentityUser<Guid>, IBaseEntity
{
    public DateTime CreateTime { get; set; }
    public DateTime? DeleteTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public override required string Email { get; set; }
    public override string? UserName => Email;
    public required string FullName { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpirationTime { get; set; }
    public bool IsExpired => DateTime.UtcNow > RefreshTokenExpirationTime;
    public ICollection<AdmissionUserRole> UserRoles { get; set; }
}