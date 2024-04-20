using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Domain.Events.EducationProgram;

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
        });

        return Task.CompletedTask;
    }
}