using Admission.Document.Domain.Enums;
using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public abstract class Document: AggregateRoot
{
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
    public ICollection<File> Files { get; } = new List<File>();
    public DocumentType Type { get; set; }
}