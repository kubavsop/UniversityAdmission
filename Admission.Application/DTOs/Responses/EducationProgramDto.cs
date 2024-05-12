using Admission.Application.Common.Mapping;
using Admission.Domain.Entities;

namespace Admission.Application.DTOs.Responses;

public sealed class EducationProgramDto: IMapFrom<EducationProgram>
{
    public required string Name { get; set; }

    public required string Code { get; set; }
    
    public required string Language { get; set; }
    
    public required string EducationForm { get; set; }

    public FacultyDto Faculty { get; set; } = null!;
    
    public EducationLevelDto EducationLevel { get; set; } = null!;
}