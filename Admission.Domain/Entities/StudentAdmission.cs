using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;
using Admission.Domain.Events.Admission;
using MediatR;

namespace Admission.Domain.Entities;

public sealed class StudentAdmission: AggregateRoot
{
    public Manager? Manager { get; private set; }
    public Guid? ManagerId { get; private set; }
    public Guid ApplicantId { get; private set; }
    public Applicant Applicant { get; private set; } = null!;
    public Guid AdmissionGroupId { get; private set; }
    public AdmissionGroup AdmissionGroup { get; private set; } = null!;
    public AdmissionStatus Status { get; private set; }
    public ICollection<EducationProgram> EducationPrograms { get; } = new List<EducationProgram>();
    public ICollection<AdmissionProgram> AdmissionPrograms { get; } = new List<AdmissionProgram>();

    private StudentAdmission()
    {
    }
    
    public static StudentAdmission Create(Applicant applicant, Guid groupId)
    {
        var studentAdmission = new StudentAdmission
        {
            Id = Guid.NewGuid(),
            ApplicantId = applicant.Id,
            Applicant = applicant,
            Status = AdmissionStatus.Created,
            AdmissionGroupId = groupId
        };
        
        studentAdmission.AddDomainEvent(new AdmissionCreatedDomainEvent(studentAdmission));

        return studentAdmission;
    }

    public void ChangeManager(Manager? manager)
    {
        if (ManagerId == manager?.Id) return;
        Manager = manager;

        if (manager != null)
        {
            ChangeStatus(AdmissionStatus.UnderReview);
        }
        
        AddDomainEvent(new AdmissionManagerChangedDomainEvent(this));
    }

    public void ChangeStatus(AdmissionStatus status)
    {
        if (Status == status) return;
        Status = status;
        
        AddDomainEvent(new AdmissionStatusChangedDomainEvent(this));
    }
}