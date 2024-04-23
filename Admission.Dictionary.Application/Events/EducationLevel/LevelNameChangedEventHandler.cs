using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationLevel;
using Admission.IntegrationEvents.Events.EducationLevel;
using IntegrationEvents;

namespace Admission.Dictionary.Application.Events.EducationLevel;

public class LevelNameChangedEventHandler: BaseDomainEventHandler<LevelNameChangedDomainEvent>
{
    public LevelNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(LevelNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new LevelNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        }, RoutingKeys.LevelChangedRoutingKey);

        return Task.CompletedTask;
    }
}