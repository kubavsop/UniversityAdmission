using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class Manager: BaseEntity
{
    public required string Email { get; set; }
    public Guid? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
}