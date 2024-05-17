using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public sealed class File: BaseEntity
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; } = null!;
    public required string Extension { get; set; }
    public required string Name { get; set; }
    public long Size { get; set; } 
}