using Admission.Document.Domain.Enums;
using Admission.Document.Domain.Events.EducationDocument;

namespace Admission.Document.Domain.Entities;

public sealed class EducationDocument: Document
{
    public string Name { get; private set; }
    public Guid EducationDocumentTypeId { get; private set; }
    public EducationDocumentType EducationDocumentType { get; private set; } = null!;

    private EducationDocument()
    {
    }

    public static EducationDocument Create(string name, Guid documentTypeId, Guid applicantId)
    {
        var educationDocument = new EducationDocument
        {
            Id = Guid.NewGuid(),
            Name = name,
            EducationDocumentTypeId = documentTypeId,
            ApplicantId = applicantId,
            Type = DocumentType.EducationDocument
        };
        
        educationDocument.AddDomainEvent(new EducationDocumentCreatedDomainEvent(educationDocument));

        return educationDocument;
    }

    public void ChangeDocumentType(Guid documentTypeId)
    {
        if (EducationDocumentTypeId == documentTypeId) return;

        EducationDocumentTypeId = documentTypeId;
        
        AddDomainEvent(new EducationDocumentTypeChangedDomainEvent(this));
    }

    public void ChangeName(string name)
    {
        if (Name == name) return;

        Name = name;
        
        AddDomainEvent(new EducationDocumentNameChangedDomainEvent(this));
    }
}