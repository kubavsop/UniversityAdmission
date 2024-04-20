using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationLevel;
using Admission.Dictionary.Domain.Events.EducationLevel;

namespace Admission.Dictionary.Application.Events.EducationLevel;

public sealed class LevelDeleteTimeChangedEventHandler: BaseDomainEventHandler<EducationLevelDeleteTimeChangedDomainEvent>
{
    public LevelDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(EducationLevelDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new LevelDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}