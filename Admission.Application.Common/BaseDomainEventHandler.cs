using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Domain.Common.Events;

namespace Admission.Application.Common;

public abstract class BaseDomainEventHandler<TDomainEvent>: IDomainEventHandler<TDomainEvent> where TDomainEvent: IDomainEvent
{
    protected readonly IIntegrationEventPublisher Publisher;

    protected BaseDomainEventHandler(IIntegrationEventPublisher publisher)
    {
        Publisher = publisher;
    }

    public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
}