using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentTypeChangedDomainEvent: IDomainEvent
{
    public EducationDocumentTypeChangedDomainEvent()
    {
    }

    internal EducationDocumentTypeChangedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Document = educationDocument;
    }
    
    public Entities.EducationDocument Document;
}