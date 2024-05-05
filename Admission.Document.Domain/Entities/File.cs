using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public sealed class File: BaseEntity
{
    public required string Name { get; set; }
    public int Size { get; set; }
}