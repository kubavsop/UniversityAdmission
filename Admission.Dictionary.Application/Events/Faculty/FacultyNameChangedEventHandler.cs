using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.Faculty;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;

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
        }, RoutingKeys.FacultyNameChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}