using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public class EducationDocumentNameChangedDomainEvent: IDomainEvent
{
    public EducationDocumentNameChangedDomainEvent()
    {
    }
    
    internal EducationDocumentNameChangedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Document = educationDocument;
    }

    public Entities.EducationDocument Document;
}