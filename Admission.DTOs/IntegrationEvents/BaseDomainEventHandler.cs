using Admission.Domain.Common.Events;

namespace Admission.DTOs.IntegrationEvents;

public abstract class BaseDomainEventHandler<TDomainEvent>: IDomainEventHandler<TDomainEvent> where TDomainEvent: IDomainEvent
{
    protected readonly IIntegrationEventPublisher Publisher;

    protected BaseDomainEventHandler(IIntegrationEventPublisher publisher)
    {
        Publisher = publisher;
    }

    public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
}