using System.ComponentModel.DataAnnotations;

namespace Admission.Application.DTOs.Requests;

public sealed class EditProgramsDto
{
    [Required]
    [Length(1,5)]
    public required IEnumerable<EditProgramDto> EditPrograms { get; init; }
}