using System.ComponentModel.DataAnnotations;

namespace Admission.Application.DTOs.Requests;

public sealed class CreateProgramDto
{
    [Required]
    [Range(0, Int32.MaxValue)]
    public int Priority { get; init; }
    [Required]
    public Guid EducationProgramId { get; init; }
}