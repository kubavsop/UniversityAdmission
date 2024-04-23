using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.IntegrationEvents.Events.EducationProgram;
using IntegrationEvents;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramDeleteTimeChangedEventHandler: BaseDomainEventHandler<ProgramDeleteTimeChangedDomainEvent>
{
    public ProgramDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(ProgramDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}