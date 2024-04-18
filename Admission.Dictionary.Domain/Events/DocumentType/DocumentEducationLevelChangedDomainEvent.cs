using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.DocumentType;

public sealed class DocumentEducationLevelChangedDomainEvent: IDomainEvent
{
    public DocumentEducationLevelChangedDomainEvent()
    {
    }

    internal DocumentEducationLevelChangedDomainEvent(EducationDocumentType documentType, Entities.EducationLevel level)
    {
        Id = documentType.Id;
        EducationLevelId = level.ExternalId;
        EducationLevelName = level.Name;
    }
    
    public Guid Id { get; init; }
    public int EducationLevelId { get; init; }
    public string EducationLevelName { get; init; }
}