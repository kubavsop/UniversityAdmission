﻿using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;

namespace Admission.Dictionary.Application.Events.DocumentType;

public sealed class DocumentEducationLevelChangedEventHandler: BaseDomainEventHandler<DocumentEducationLevelChangedDomainEvent>
{
    public DocumentEducationLevelChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }
    
    public override Task Handle(DocumentEducationLevelChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new DocumentEducationLevelChangedIntegrationEvent
        {
            Id = notification.Id,
            EducationLevelId = notification.EducationLevelId,
            EducationLevelName = notification.EducationLevelName
        }, RoutingKeys.DocumentChangedRoutingKey);
        
        return Task.CompletedTask;
    }
}