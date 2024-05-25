using System.Collections;
using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.Domain.Entities;

public sealed class AdmissionGroup: BaseEntity
{
    public AdmissionGroupStatus Status { get; set; }
    public ICollection<StudentAdmission> Admissions { get; }
}