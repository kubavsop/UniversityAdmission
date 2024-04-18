using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Faculty;
using Admission.Dictionary.Domain.Events.Faculty;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Application.Events.Faculty;

public sealed class FacultyDeleteTimeChangedEventHandler: IDomainEventHandler<FacultyDeleteTimeChangedDomainEvent>
{
    private readonly IIntegrationEventPublisher _publisher;

    public FacultyDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(FacultyDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(new FacultyDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        });

        return Task.CompletedTask;
    }
}