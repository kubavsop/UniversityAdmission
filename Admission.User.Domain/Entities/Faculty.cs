using Admission.Domain.Common.Entities;

namespace Admission.User.Domain.Entities;

public sealed class Faculty: BaseEntity
{
    public required string Name { get; set; }
}