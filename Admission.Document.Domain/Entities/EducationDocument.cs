namespace Admission.Document.Domain.Entities;

public sealed class EducationDocument: Document
{
    public required string Name { get; set; }
    public Guid EducationDocumentTypeId { get; set; }
    public EducationDocumentType EducationDocumentType { get; set; } = null!;

    private EducationDocument()
    {
    }

    public static EducationDocument Create(string name, EducationDocumentType documentType)
    {
        var educationDocument = new EducationDocument
        {
            Name = name,
            EducationDocumentTypeId = documentType.Id
        };
        throw new NotImplementedException();
    }
}