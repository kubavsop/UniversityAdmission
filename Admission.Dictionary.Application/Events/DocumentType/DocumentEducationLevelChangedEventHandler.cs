using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.DocumentType;
using Admission.Dictionary.Domain.Events.DocumentType;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentEducationLevelChangedEventHandler: BaseDomainEventHandler<DocumentEducationLevelChangedDomainEvent>
{
    public DocumentEducationLevelChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    
    public override Task Handle(DocumentEducationLevelChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new DocumentEducationLevelChangedIntegrationEvent
        {
            Id = notification.Id,
            EducationLevelId = notification.EducationLevelId,
            EducationLevelName = notification.EducationLevelName
        });
        
        return Task.CompletedTask;
    }
}