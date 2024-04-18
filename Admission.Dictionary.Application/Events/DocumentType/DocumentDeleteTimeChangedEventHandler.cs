using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentDeleteTimeChangedEventHandler: IDomainEventHandler<DocumentDeleteTimeChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public DocumentDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(DocumentDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new DocumentDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}