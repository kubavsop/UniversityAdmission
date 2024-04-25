using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramFacultyChangedEventHandler: BaseDomainEventHandler<ProgramFacultyChangedDomainEvent>
{
    public ProgramFacultyChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramFacultyChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramFacultyChangedIntegrationEvent
        {
            Id = notification.Id,
            FacultyName = notification.FacultyName,
            FacultyId = notification.FacultyId
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}