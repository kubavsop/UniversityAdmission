using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class Faculty: BaseEntity
{
    public required string Name { get; set; }
}