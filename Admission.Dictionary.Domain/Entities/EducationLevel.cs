using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationLevel: BaseEntity
{
    public int ExternalId { get; set; }
    public required string Name { get; set; }
    public ICollection<EducationDocumentType> DocumentTypes { get; set; } = new List<EducationDocumentType>();
}