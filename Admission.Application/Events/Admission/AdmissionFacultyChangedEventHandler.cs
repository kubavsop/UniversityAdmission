using Admission.Application.Constants;
using Admission.Domain.Events.Admission;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

namespace Admission.Application.Events.Admission;

public sealed class AdmissionFacultyChangedEventHandler: BaseDomainEventHandler<AdmissionFacultyChangedDomainEvent>
{
    public AdmissionFacultyChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(AdmissionFacultyChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new AdmissionFacultyChangedIntegrationEvent
        {
            Id = notification.Id,
            FirstPriorityFacultyName = notification.FirstPriorityFacultyName,
            FirstPriorityFacultyId = notification.FirstPriorityFacultyId
        }, RoutingKeys.AdmissionChangedRoutingKey);

        return Task.CompletedTask;
    }
}