namespace Admission.User.Infrastructure.Options;

public sealed class AdminOptions
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Name { get; init; }
}