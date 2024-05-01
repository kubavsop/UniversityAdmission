namespace Admission.User.Application.Options;

public sealed class RefreshTokenOptions
{
    public int RefreshTokenExpirationHours { get; init; }
    public int RefreshTokenBytes { get; init; }
}