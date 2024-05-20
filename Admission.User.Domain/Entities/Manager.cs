using Admission.Domain.Common.Entities;
using Admission.User.Domain.Events.Manager;

namespace Admission.User.Domain.Entities;

public sealed class Manager: AggregateRoot
{
    public AdmissionUser User { get; set; } = null!;
    public Guid? FacultyId { get; set; }
    public Faculty? Faculty { get; private set; }
    
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();


    private Manager()
    {
    }
    
    public void ChangeEmail(string email)
    {
        if (User.Email == email) return;
        User.Email = email;
        
        AddDomainEvent(new ManagerEmailChangedDomainEvent(User));
    }

    public void ChangeFullname(string fullname)
    {
        if (User.FullName == fullname) return;
        User.FullName = fullname;
        
        AddDomainEvent(new ManagerFullNameChangedDomainEvent(User));
    }

    public void ChangeFaculty(Faculty? faculty)
    {
        if (Faculty == faculty) return;
        
        Faculty = faculty;
        FacultyId = faculty?.Id;
        
        AddDomainEvent(new ManagerFacultyChangedDomainEvent(this));
    }

    public static Manager Create(AdmissionUser user)
    {
        var manager = new Manager
        {
            User = user
        };
        
        manager.AddDomainEvent(new ManagerCreatedDomainEvent(user));

        return manager;
    }
}