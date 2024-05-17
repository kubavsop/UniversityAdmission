using Admission.DTOs.IntegrationEvents;
using MediatR;

namespace Admission.RabbitMQ.Services.Impl;

public sealed class IntegrationEventConsumer: IIntegrationEventConsumer
{
    private readonly IPublisher _publisher;

    public IntegrationEventConsumer(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public void Consume(IIntegrationEvent integrationEvent) => _publisher.Publish(integrationEvent).GetAwaiter().GetResult();
}