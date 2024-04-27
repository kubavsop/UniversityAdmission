using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class EducationLevel: BaseEntity
{
    public int ExternalId { get; set; }
    
    public required string Name { get; set; }
    
    public ICollection<Applicant> Applicants { get; } = new List<Applicant>();
    public ICollection<NextEducationLevel> NextEducationLevels { get; } = new List<NextEducationLevel>();
}