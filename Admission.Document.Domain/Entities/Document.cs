using Admission.Document.Domain.Enums;
using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public abstract class Document: AggregateRoot
{
    public ICollection<File> Files { get; } = new List<File>();
    public DocumentType Type { get; set; }
}