namespace Admission.Infrastructure.Common.Messaging.Options;

public sealed class IntegrationQueuesOptions
{
    public required string ExchangeName { get; init; }
    public required List<TopicQueue> Queues { get; init; }
}