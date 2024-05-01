using Admission.Domain.Common.Entities;

namespace Admission.User.Domain.Entities;

public sealed class RefreshToken: BaseEntity
{
    public Guid UserId { get; set; }

    public AdmissionUser User { get; set; } = null!;
    public required string Token { get; set; }
    
    public Guid AccessTokenId { get; set; }
    public DateTime? RefreshTokenExpirationTime { get; set; }
    
    public bool RefreshTokenIsExpired => DateTime.UtcNow > RefreshTokenExpirationTime;
}