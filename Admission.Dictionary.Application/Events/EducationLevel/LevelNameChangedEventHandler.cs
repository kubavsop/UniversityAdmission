using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationLevel;
using Admission.Dictionary.Domain.Events.EducationLevel;

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
        });

        return Task.CompletedTask;
    }
}