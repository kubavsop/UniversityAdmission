using IntegrationEvents;

namespace Admission.IntegrationEvents.Events.Applicant;

public sealed class ApplicantCreatedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    
    public required string Email { get; init; }

    public required string FullName { get; init; }
}