using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.User.Domain.Entities;

public sealed class Applicant: BaseEntity
{
    public AdmissionUser? User { get; set; }
    public AdmissionStatus Status { get; set; }
    public Guid? FirstPriorityFacultyId { get; set; }
    public Faculty? FirstPriorityFaculty;
    
    public Manager Manager { get; set; }
    public Guid ManagerId { get; set; }
}