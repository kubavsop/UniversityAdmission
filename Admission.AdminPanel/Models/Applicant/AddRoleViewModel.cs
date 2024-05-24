using Admission.Domain.Common.Enums;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class AddRoleViewModel
{
    public required Guid ApplicantId { get; init; }
    public RoleType RoleToAdd { get; init; }
}