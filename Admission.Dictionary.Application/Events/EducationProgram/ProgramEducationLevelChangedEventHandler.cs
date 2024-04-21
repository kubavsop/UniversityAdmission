using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.IntegrationEvents.Events.EducationProgram;
using IntegrationEvents;

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
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}