using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Admission.User.Application.Constants;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Application.Events.Manager;

public sealed class ManagerFacultyChangedEventHandler: BaseDomainEventHandler<ManagerFacultyChangedDomainEvent>
{
    public ManagerFacultyChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ManagerFacultyChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ManagerFacultyChangedIntegrationEvent
        {
            Id = notification.Id,
            FacultyId = notification.Faculty?.Id,
            FacultyName = notification.Faculty?.Name
        }, RoutingKeys.ManagerFacultyChangedRoutingKey);

        return Task.CompletedTask;
    }
}