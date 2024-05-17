using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class EducationDocumentType: BaseEntity
{
    public int EducationLevelId { get; set; }
    public EducationLevel EducationLevel { get; set; } = null!;
    public ICollection<EducationLevel> EducationLevels { get; } = new List<EducationLevel>();
    public ICollection<NextEducationLevel> NextEducationLevels { get; } = new List<NextEducationLevel>();
}