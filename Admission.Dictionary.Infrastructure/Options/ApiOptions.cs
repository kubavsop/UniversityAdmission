namespace Admission.Dictionary.Infrastructure.Options;

public sealed class ApiOptions
{
    public required string BaseUrl { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}