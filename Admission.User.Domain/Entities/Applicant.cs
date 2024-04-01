using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.User.Domain.Entities;

public sealed class Applicant: BaseEntity
{
    public DateTime? Birthday { get; set; }
    public string? PhoneNumber { get; set; } 
    public string? Citizenship { get; set; }
    public Gender? Gender { get; set; }
    public AdmissionUser? User { get; set; }
    public StudentAdmission? StudentAdmission { get; set; }
}