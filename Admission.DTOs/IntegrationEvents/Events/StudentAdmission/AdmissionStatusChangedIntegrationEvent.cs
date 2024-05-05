using Admission.Domain.Common.Enums;

namespace Admission.DTOs.IntegrationEvents.Events.StudentAdmission;

public sealed class AdmissionStatusChangedIntegrationEvent : IIntegrationEvent
{
    public required Guid Id { get; init; }
    public AdmissionStatus Status { get; set; }   
}