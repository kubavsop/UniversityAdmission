using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.EducationDocument;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Document.Application.Events.EducationDocument;

public sealed class EducationDocumentTypeChangedEventHandler: BaseDomainEventHandler<EducationDocumentTypeChangedDomainEvent>
{
    public EducationDocumentTypeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(EducationDocumentTypeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new EducationDocumentTypeChangedIntegrationEvent
        {
            Id = notification.Id,
            EducationDocumentTypeId = notification.EducationDocumentTypeId
        }, RoutingKeys.DataChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}