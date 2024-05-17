using Admission.Domain.Common.Events;

namespace Admission.Document.Domain.Events.EducationDocument;

public sealed class EducationDocumentCreatedDomainEvent: IDomainEvent
{
    public EducationDocumentCreatedDomainEvent()
    {
    }

    internal EducationDocumentCreatedDomainEvent(Entities.EducationDocument educationDocument)
    {
        Id = educationDocument.Id;
        Name = educationDocument.Name;
        EducationDocumentTypeId = educationDocument.EducationDocumentTypeId;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid EducationDocumentTypeId { get; init; }}