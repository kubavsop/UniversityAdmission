using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentCreatedDomainEvent: IDomainEvent
{
    public EducationDocumentCreatedDomainEvent()
    {
    }

    internal EducationDocumentCreatedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Document = educationDocument;
    }

    public Entities.EducationDocument Document;
}