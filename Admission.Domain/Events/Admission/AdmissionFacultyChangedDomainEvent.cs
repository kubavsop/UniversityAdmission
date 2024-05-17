using Admission.Domain.Common.Events;

namespace Admission.Domain.Events.Admission;

public sealed class AdmissionFacultyChangedDomainEvent: IDomainEvent
{
    public AdmissionFacultyChangedDomainEvent()
    {
    }

    internal AdmissionFacultyChangedDomainEvent(Guid admissionId, Guid facultyId, string facultyName)
    {
        Id = admissionId;
        FirstPriorityFacultyId = facultyId;
        FirstPriorityFacultyName = facultyName;
    }
    
    public Guid Id { get; init; }
    public Guid? FirstPriorityFacultyId { get; set; }
    public string FirstPriorityFacultyName { get; set; }
}