using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.EducationDocument;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Document.Application.Events.EducationDocument;

public sealed class EducationDocumentNameChangedEventHandler: BaseDomainEventHandler<EducationDocumentNameChangedDomainEvent>
{
    public EducationDocumentNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(EducationDocumentNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new EducationDocumentNameChangedIntegrationEvent
        {
            Id = notification.Document.Id,
            Name = notification.Document.Name
        }, RoutingKeys.DataChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}