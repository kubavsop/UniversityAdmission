using Admission.Domain.Common.Entities;
using Admission.Domain.Common.Enums;
using Admission.User.Domain.Events;
using Admission.User.Domain.Events.Applicant;

namespace Admission.User.Domain.Entities;

public sealed class Applicant: AggregateRoot
{
    public DateTime? Birthday { get; private set; }
    public string? PhoneNumber { get; private set; } 
    public string? Citizenship { get; private set; }
    public Gender? Gender { get; private set; }
    public AdmissionUser User { get; set; } = null!;
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();

    private Applicant()
    {
    }

    public void ChangeEmail(string email)
    {
        if (User.Email == email) return;
        User.Email = email;
        
        AddDomainEvent(new ApplicantEmailChangedDomainEvent(User));
    }

    public void ChangeFullname(string fullname)
    {
        if (User.FullName == fullname) return;
        User.FullName = fullname;
        
        AddDomainEvent(new ApplicantFullNameChangedDomainEvent(User));
    }

    public void ChangeBirthday(DateTime? birthday)
    {
        if (birthday == Birthday) return;
        
        Birthday = birthday;
        
        AddDomainEvent(new ApplicantChangedDomainEvent(this));
    }
    
    public void ChangePhoneNumber(string? phoneNumber)
    {
        if (phoneNumber == PhoneNumber) return;
        
        PhoneNumber = phoneNumber;
        
        AddDomainEvent(new ApplicantChangedDomainEvent(this));
    }
    
    public void ChangeGender(Gender? gender)
    {
        if (gender == Gender) return;
        
        Gender = gender;
        
        AddDomainEvent(new ApplicantChangedDomainEvent(this));
    }
    
    public void ChangeCitizenship(string? citizenship)
    {
        if (citizenship == Citizenship) return;
        
        Citizenship = citizenship;
        
        AddDomainEvent(new ApplicantChangedDomainEvent(this));
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