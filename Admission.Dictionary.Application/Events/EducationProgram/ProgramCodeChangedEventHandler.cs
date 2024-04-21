using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramCodeChangedEventHandler: BaseDomainEventHandler<ProgramCodeChangedDomainEvent>
{
    public ProgramCodeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramCodeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramCodeChangedIntegrationEvent
        {
            Id = notification.Id,
            Code = notification.Code
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}