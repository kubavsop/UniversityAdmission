using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationDocumentType: BaseEntity
{
    public required string Name { get; set; }
    
    public Guid EducationLevelId { get; set; }
    
    public EducationLevel? EducationLevel { get; set; }

    public ICollection<EducationLevel> EducationLevels { get; set; } = new List<EducationLevel>();
}