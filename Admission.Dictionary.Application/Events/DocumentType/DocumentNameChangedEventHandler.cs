using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentNameChangedEventHandler: IDomainEventHandler<DocumentNameChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public DocumentNameChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }


    public Task Handle(DocumentNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new DocumentNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        });
        
        return Task.CompletedTask;
    }
}