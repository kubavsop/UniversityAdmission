namespace Admission.Infrastructure.Common.Messaging.Options;

public sealed class TopicQueue
{
    public required string Name { get; set; }
    public required string RoutingKey { get; set; }
}