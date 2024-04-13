using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationLevel
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    public ICollection<EducationDocumentType> DocumentTypes { get; set; } = new List<EducationDocumentType>();
}