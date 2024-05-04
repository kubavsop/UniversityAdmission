using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public sealed class Applicant: BaseEntity
{
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
}