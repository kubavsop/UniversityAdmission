using Admission.Document.Application.Constants;
using Admission.Document.Domain.Events.File;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.FIle;

namespace Admission.Document.Application.Events.File;

public sealed class FileChangedEventHandler: BaseDomainEventHandler<FileChangedDomainEvent>
{
    public FileChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(FileChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new FileChangedIntegrationEvent
        {
            UserId = notification.UserId
        }, RoutingKeys.DataChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}