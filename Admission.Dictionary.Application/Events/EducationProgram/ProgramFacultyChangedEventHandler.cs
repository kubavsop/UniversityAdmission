using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Domain.Events.EducationProgram;

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
        });

        return Task.CompletedTask;
    }
}