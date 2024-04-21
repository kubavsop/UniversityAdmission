using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramEducationFormChangeEventHandler: BaseDomainEventHandler<ProgramEducationFormChangeDomainEvent>
{
    public ProgramEducationFormChangeEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramEducationFormChangeDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramEducationFormChangeIntegrationEvent
        {
            Id = notification.Id,
            EducationForm = notification.EducationForm
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}