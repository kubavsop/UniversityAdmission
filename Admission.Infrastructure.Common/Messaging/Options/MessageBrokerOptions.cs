namespace Admission.Infrastructure.Common.Messaging.Options;

public sealed class MessageBrokerOptions
{
    public required string HostName { get; init; }
    public required int Port { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}