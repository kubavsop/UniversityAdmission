using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.Faculty;
using Admission.IntegrationEvents.Events.Faculty;
using IntegrationEvents;

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
        }, RoutingKeys.FacultyChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}