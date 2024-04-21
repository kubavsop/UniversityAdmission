using Admission.Application.Common;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.EducationProgram;
using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;

namespace Admission.Dictionary.Application.Events.EducationProgram;

public sealed class ProgramLanguageChangedEventHandler: BaseDomainEventHandler<ProgramLanguageChangedDomainEvent>
{
    public ProgramLanguageChangedEventHandler(IIntegrationEventPublisher publisher) : base(publisher)
    {
    }

    public override Task Handle(ProgramLanguageChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        Publisher.Publish(new ProgramLanguageChangedIntegrationEvent
        {
            Id = notification.Id,
            Language = notification.Language
        }, RoutingKeys.ProgramChangedRoutingKey);

        return Task.CompletedTask;
    }
}