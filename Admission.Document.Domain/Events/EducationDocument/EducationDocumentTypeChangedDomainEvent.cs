using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentTypeChangedDomainEvent: IDomainEvent
{
    public EducationDocumentTypeChangedDomainEvent()
    {
    }

    internal EducationDocumentTypeChangedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Id = educationDocument.Id;
        EducationDocumentTypeId = educationDocument.EducationDocumentTypeId;
        UserId = educationDocument.ApplicantId;
    }
    
    public Guid Id { get; init; }
    public Guid EducationDocumentTypeId { get; init; }
    public Guid UserId { get; init; }
}