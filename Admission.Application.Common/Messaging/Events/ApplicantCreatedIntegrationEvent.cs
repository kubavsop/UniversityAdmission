namespace Admission.Application.Common.Messaging.Events;

public sealed class ApplicantCreatedIntegrationEvent: IIntegrationEvent
{
    public required Guid Id { get; init; }
    
    public required string Email { get; init; }

    public required string FullName { get; init; }
}