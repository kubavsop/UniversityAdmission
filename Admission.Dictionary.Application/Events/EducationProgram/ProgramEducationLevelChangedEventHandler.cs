using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Domain.Events.EducationProgram;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramEducationLevelChangedEventHandler: BaseDomainEventHandler<ProgramEducationLevelChangedDomainEvent>
{
    public ProgramEducationLevelChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramEducationLevelChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramEducationLevelChangedIntegrationEvent
        {
            Id = notification.Id,
            EducationLevelId = notification.EducationLevelId,
            EducationLevelName = notification.EducationLevelName
        });

        return Task.CompletedTask;
    }
}