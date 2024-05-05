using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public sealed class Manager: BaseEntity
{
    public Guid? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
}