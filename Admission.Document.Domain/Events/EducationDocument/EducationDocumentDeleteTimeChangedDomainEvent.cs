using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentDeleteTimeChangedDomainEvent: IDomainEvent
{
    public EducationDocumentDeleteTimeChangedDomainEvent()
    {
    }

    internal EducationDocumentDeleteTimeChangedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Id = educationDocument.Id;
        DeleteTime = educationDocument.DeleteTime;
        UserId = educationDocument.ApplicantId;
    }
    
    public Guid Id { get; init; }
    public DateTime? DeleteTime { get; init; }
    public Guid UserId { get; init; }
}