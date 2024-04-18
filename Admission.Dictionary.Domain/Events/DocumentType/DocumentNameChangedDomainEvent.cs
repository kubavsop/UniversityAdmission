using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.DocumentType;

public sealed class DocumentNameChangedDomainEvent: IDomainEvent
{

    public DocumentNameChangedDomainEvent()
    {
    }
    internal DocumentNameChangedDomainEvent(EducationDocumentType documentType)
    {
        Id = documentType.Id;
        Name = documentType.Name;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
}

