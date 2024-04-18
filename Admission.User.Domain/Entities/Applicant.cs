using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;
using Admission.User.Domain.Events;

namespace Admission.User.Domain.Entities;

public sealed class Applicant: AggregateRoot
{
    public DateTime? Birthday { get; set; }
    public string? PhoneNumber { get; set; } 
    public string? Citizenship { get; set; }
    public Gender? Gender { get; set; }
    public AdmissionUser User { get; set; } = null!;
    public StudentAdmission? StudentAdmission { get; set; }

    private Applicant()
    {
    }
    
    public static Applicant Create(
        DateTime? birthday, 
        string? phoneNumber, 
        string? citizenship, 
        Gender? gender, 
        AdmissionUser user)
    {
        var applicant = new Applicant
        {
            Birthday = birthday,
            PhoneNumber = phoneNumber,
            Citizenship = citizenship,
            Gender = gender,
            User = user
        };
        
        applicant.AddDomainEvent(new ApplicantCreatedDomainEvent(user));

        return applicant;
    }
}