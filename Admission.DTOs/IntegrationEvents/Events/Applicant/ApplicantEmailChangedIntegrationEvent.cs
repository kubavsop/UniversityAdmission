namespace Admission.DTOs.IntegrationEvents.Events.Applicant;

public sealed class ApplicantEmailChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    
    public required string Email { get; init; }
}