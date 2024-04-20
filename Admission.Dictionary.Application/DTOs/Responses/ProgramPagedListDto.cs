using System.ComponentModel.DataAnnotations;

namespace Admission.Dictionary.Application.DTOs.Responses;

public sealed class ProgramPagedListDto
{
    [Required]
    public required IEnumerable<EducationProgramDto> Programs { get; set; }
    
    [Required]
    public required PageInfoDto Pagination { get; set; }
}