using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentEducationLevelChangedEventHandler: IDomainEventHandler<DocumentEducationLevelChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public DocumentEducationLevelChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }


    public Task Handle(DocumentEducationLevelChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new DocumentEducationLevelChangedIntegrationEvent
        {
            Id = notification.Id,
            EducationLevelId = notification.EducationLevelId,
            EducationLevelName = notification.EducationLevelName
        });
        
        return Task.CompletedTask;
    }
}