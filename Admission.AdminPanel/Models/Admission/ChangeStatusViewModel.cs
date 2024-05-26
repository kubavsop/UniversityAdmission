using Admission.Domain.Common.Enums;

namespace Admission.AdminPanel.Models.Admission;

public sealed class ChangeStatusViewModel
{
    public required Guid AdmissionId { get; init; }
    public required AdmissionStatus Status { get; init; }
}