namespace Admission.Application.DTOs.Requests;

public sealed class EditProgramDto
{
    public int Priority { get; init; }
    public Guid AdmissionProgramId { get; init; }
}