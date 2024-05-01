namespace Admission.User.Application.Models;

public sealed class TokenIdentifiers
{
    public Guid UserId { get; init; }
    public Guid TokenId { get; init; }
}