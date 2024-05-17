namespace Admission.DTOs.IntegrationEvents.Events.Passport;

public sealed class PassportChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
}