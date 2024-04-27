using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class EducationProgram: BaseEntity
{
    public required string Name { get; set; }

    public required string Code { get; set; }
    
    public required string Language { get; set; }
    
    public required string EducationForm { get; set; }
    
    public Guid FacultyId { get; set; }
    
    public int EducationLevelId { get; set; }

    public Faculty Faculty { get; set; } = null!;
    
    public EducationLevel EducationLevel { get; set; } = null!;

    public ICollection<StudentAdmission> StudentAdmissions { get; } = new List<StudentAdmission>();
    public ICollection<AdmissionProgram> AdmissionPrograms { get; } = new List<AdmissionProgram>();
}