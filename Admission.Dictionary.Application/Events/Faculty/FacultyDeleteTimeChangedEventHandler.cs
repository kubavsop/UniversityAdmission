using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.Faculty;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;

namespace Admission.Dictionary.Application.Events.Faculty;

public sealed class FacultyDeleteTimeChangedEventHandler: BaseDomainEventHandler<FacultyDeleteTimeChangedDomainEvent>
{
    public FacultyDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(FacultyDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new FacultyDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.FacultyDeleteTimeChangedRoutingKey);

        return Task.CompletedTask;
    }
}