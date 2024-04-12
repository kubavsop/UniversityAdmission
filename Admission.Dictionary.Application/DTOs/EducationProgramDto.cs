using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Domain.Entities;

namespace Admission.Dictionary.Application.DTOs;

public sealed class EducationProgramDto: IMapFrom<EducationProgram>
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required DateTime CreateTime { get; set; }

    [Required]
    [MinLength(1)]
    public required string Name { get; set; }

    [Required]
    public required string Code { get; set; }

    [Required]
    public required string Language { get; set; }

    [Required]
    public required string EducationForm { get; set; }

    [Required]
    public required FacultyDto Faculty { get; set; }

    [Required]
    public required EducationLevelDto EducationLevel { get; set; }
}