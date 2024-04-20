using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentNameChangedEventHandler: BaseDomainEventHandler<DocumentNameChangedDomainEvent>
{
    public DocumentNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(DocumentNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new DocumentNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        });
        
        return Task.CompletedTask;
    }
}