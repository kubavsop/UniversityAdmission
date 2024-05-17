namespace Admission.Application.DTOs.Requests;

public sealed class CreateProgramDto
{
    public int Priority { get; init; }
    public Guid EducationProgramId { get; init; }
}