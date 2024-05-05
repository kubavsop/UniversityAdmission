namespace Admission.Document.Domain.Entities;

public sealed class EducationDocument: Document
{
    public required string Name { get; set; }
    public Guid EducationDocumentTypeId { get; set; }
    public EducationDocumentType EducationDocumentType { get; set; } = null!;
}