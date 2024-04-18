using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramDeleteTimeChangedEventHandler: IDomainEventHandler<ProgramDeleteTimeChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public ProgramDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(ProgramDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new ProgramDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}