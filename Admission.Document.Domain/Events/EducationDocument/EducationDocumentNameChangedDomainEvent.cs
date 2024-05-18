using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentNameChangedDomainEvent: IDomainEvent
{
    public EducationDocumentNameChangedDomainEvent()
    {
    }
    
    internal EducationDocumentNameChangedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Id = educationDocument.Id;
        Name = educationDocument.Name;
        UserId = educationDocument.ApplicantId;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid UserId { get; init; }
}