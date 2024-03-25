using Admission.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Domain.Entities;

public sealed class AdmissionUser: IdentityUser<Guid>, IBaseEntity
{
    public DateTime CreateTime { get; set; }
    public DateTime? DeleteTime { get; set; }
    
    public required string FullName { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpirationTime { get; set; } = null;
    
    public bool IsExpired => DateTime.UtcNow > RefreshTokenExpirationTime;
}