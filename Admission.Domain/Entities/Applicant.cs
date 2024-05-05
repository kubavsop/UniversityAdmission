using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class Applicant: BaseEntity
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
    public ICollection<EducationLevel> EducationLevels { get; } = new List<EducationLevel>();
    public ICollection<NextEducationLevel> NextEducationLevels { get; } = new List<NextEducationLevel>();
}