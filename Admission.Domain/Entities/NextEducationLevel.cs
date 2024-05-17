using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class NextEducationLevel: BaseEntity
{
    public Guid EducationDocumentTypeId { get; set; }
    public int EducationLevelId { get; set; }

    public EducationLevel EducationLevel { get; set; } = null!;
    public EducationDocumentType EducationDocumentType { get; set; } = null!;
}