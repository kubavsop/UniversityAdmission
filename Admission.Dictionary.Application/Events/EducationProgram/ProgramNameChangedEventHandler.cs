﻿using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.IntegrationEvents.Events.EducationProgram;
using IntegrationEvents;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramNameChangedEventHandler: BaseDomainEventHandler<ProgramNameChangedDomainEvent>
{
    public ProgramNameChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramNameChangedIntegrationEvent
        {
            Id = notification.Id,
            Name = notification.Name
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}