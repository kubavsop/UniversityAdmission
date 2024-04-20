using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.DocumentType;

public sealed class DocumentDeleteTimeChangedDomainEvent: DeleteTimeChangedEvent
{
    public DocumentDeleteTimeChangedDomainEvent()
    {
    }

    public DocumentDeleteTimeChangedDomainEvent(EducationDocumentType documentType) : base(documentType)
    {
    }
}