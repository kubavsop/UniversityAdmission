using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.EducationDocument;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Document.Application.Events.EducationDocument;

public sealed class EducationDocumentDeleteTimeChangedEventHandler: BaseDomainEventHandler<EducationDocumentDeleteTimeChangedDomainEvent>
{
    public EducationDocumentDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(EducationDocumentDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new EducationDocumentDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime,
            UserId = notification.UserId
        }, RoutingKeys.DataChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}