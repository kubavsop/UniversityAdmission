using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.EducationDocument;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Document.Application.Events.EducationDocument;

public sealed class EducationDocumentCreatedEventHandler: BaseDomainEventHandler<EducationDocumentCreatedDomainEvent>
{
    public EducationDocumentCreatedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(EducationDocumentCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new EducationDocumentCreatedIntegrationEvent
        {
            Id = notification.Document.Id,
            EducationDocumentTypeId = notification.Document.EducationDocumentTypeId,
            Name = notification.Document.Name
        }, RoutingKeys.DataChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}