namespace Admission.DTOs.IntegrationEvents.Events.Applicant;

public sealed class ApplicantChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
}