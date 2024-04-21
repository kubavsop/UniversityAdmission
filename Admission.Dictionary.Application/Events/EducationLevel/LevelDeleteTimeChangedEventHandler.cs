﻿using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationLevel;
using Admission.IntegrationEvents.Events.EducationLevel;
using IntegrationEvents;

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
        }, RoutingKeys.LevelChangedRoutingKey);

        return Task.CompletedTask;
    }
}