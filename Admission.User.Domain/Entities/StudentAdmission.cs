using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.User.Domain.Entities;

public class StudentAdmission: BaseEntity
{
    public Manager? Manager { get; set; }
    public Guid? ManagerId { get; set; }
    public AdmissionStatus Status { get; set; }
    public Guid? FirstPriorityFacultyId { get; set; }
    public Faculty? FirstPriorityFaculty;
    public Guid ApplicantId { get; set; }
    public Applicant? Applicant { get; set; }
}