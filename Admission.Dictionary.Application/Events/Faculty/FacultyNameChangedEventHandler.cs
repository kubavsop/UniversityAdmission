using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Faculty;
using Admission.Dictionary.Domain.Events.Faculty;

namespace Admission.Dictionary.Application.Events.Faculty;

public sealed class FacultyNameChangedEventHandler: BaseDomainEventHandler<FacultyNameChangedDomainEvent>
{
    public FacultyNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    
    public override Task Handle(FacultyNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new FacultyNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        });
        
        return Task.CompletedTask;
    }
}