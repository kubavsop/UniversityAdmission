using Admission.Dictionary.Domain.Events.Faculty;
using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class Faculty: AggregateRoot
{
    public required string Name { get; set; }
    
    public override void ChangeDeleteTime(DateTime? deleteTime)
    {
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new FacultyDeleteTimeChangedDomainEvent(this));
    }
    
    public void ChangeName(string name)
    {
        if (Name == name) return;
        Name = name;
        AddDomainEvent(new FacultyNameChangedDomainEvent(this));
    }
}