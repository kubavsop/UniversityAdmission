using Admission.Application.Common.Messaging.IntegrationEvent;
using MediatR;

namespace Admission.Infrastructure.Common.Services.Impl;

public sealed class IntegrationEventConsumer: IIntegrationEventConsumer
{
    private readonly IPublisher _publisher;

    public IntegrationEventConsumer(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public void Consume(IIntegrationEvent integrationEvent) => _publisher.Publish(integrationEvent);
}