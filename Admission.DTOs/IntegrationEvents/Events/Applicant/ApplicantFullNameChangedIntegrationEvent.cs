namespace Admission.DTOs.IntegrationEvents.Events.Applicant;

public sealed class ApplicantFullNameChangedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }

    public required string FullName { get; init; }
}