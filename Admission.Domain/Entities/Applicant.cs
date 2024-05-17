using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class Applicant: BaseEntity
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
}