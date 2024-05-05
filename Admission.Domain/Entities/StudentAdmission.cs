using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.Domain.Entities;

public sealed class StudentAdmission: AggregateRoot
{
    public Manager? Manager { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
    public Guid AdmissionGroupId { get; set; }
    public AdmissionGroup AdmissionGroup { get; set; } = null!;
    public AdmissionStatus Status { get; set; }
    public ICollection<EducationProgram> EducationPrograms { get; } = new List<EducationProgram>();
    public ICollection<AdmissionProgram> AdmissionPrograms { get; } = new List<AdmissionProgram>();
}