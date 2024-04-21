using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Faculty;
using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.Faculty;

namespace Admission.Dictionary.Application.Events.Faculty;

public sealed class FacultyDeleteTimeChangedEventHandler: BaseDomainEventHandler<FacultyDeleteTimeChangedDomainEvent>
{
    public FacultyDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    public override Task Handle(FacultyDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new FacultyDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.FacultyChangedRoutingKey);

        return Task.CompletedTask;
    }
}