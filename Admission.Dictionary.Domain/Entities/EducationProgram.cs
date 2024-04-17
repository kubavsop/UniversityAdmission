using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationProgram: BaseEntity
{
    public required string Name { get; set; }

    public required string Code { get; set; }
    
    public required string Language { get; set; }
    
    public required string EducationForm { get; set; }
    
    public Guid FacultyId { get; set; }
    
    public int EducationLevelId { get; set; }
    
    public Faculty? Faculty { get; set; }
    public EducationLevel? EducationLevel { get; set; }
}