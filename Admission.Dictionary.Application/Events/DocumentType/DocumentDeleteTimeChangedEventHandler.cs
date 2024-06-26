﻿using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentDeleteTimeChangedEventHandler: BaseDomainEventHandler<DocumentDeleteTimeChangedDomainEvent>
{
    public DocumentDeleteTimeChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    { }
    
    public override Task Handle(DocumentDeleteTimeChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new DocumentDeleteTimeChangedIntegrationEvent
        {
            Id = notification.Id,
            DeleteTime = notification.DeleteTime
        }, RoutingKeys.DocumentChangedRoutingKey);

        return Task.CompletedTask;
    }
}