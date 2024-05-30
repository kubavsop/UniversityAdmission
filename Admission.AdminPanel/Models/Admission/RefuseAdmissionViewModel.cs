namespace Admission.AdminPanel.Models.Admission;

public sealed class RefuseAdmissionViewModel
{
    public Guid AdmissionId { get; init; }
    public Guid ManagerId { get; init; }
}