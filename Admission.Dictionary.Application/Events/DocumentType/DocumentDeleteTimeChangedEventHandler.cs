using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentDeleteTimeChangedEventHandler: BaseDomainEventHandler<DocumentDeleteTimeChangedDomainEvent>
{
    public DocumentDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    { }
    
    public override Task Handle(DocumentDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new DocumentDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}