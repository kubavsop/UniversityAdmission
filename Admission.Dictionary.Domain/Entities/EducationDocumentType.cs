using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationDocumentType: BaseEntity
{
    public required string Name { get; set; }
    
    public int EducationLevelId { get; set; }
    
    public EducationLevel? EducationLevel { get; set; }

    public ICollection<EducationLevel> NextEducationLevels { get; set; } = new List<EducationLevel>();
}