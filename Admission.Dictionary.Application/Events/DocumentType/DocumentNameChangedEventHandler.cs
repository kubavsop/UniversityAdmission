using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;

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
        }, RoutingKeys.DocumentChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}