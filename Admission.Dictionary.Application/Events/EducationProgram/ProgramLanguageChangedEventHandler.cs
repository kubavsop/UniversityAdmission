using Admission.Dictionary.Application.Constants;
using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;

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