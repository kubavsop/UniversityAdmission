using Admission.Dictionary.Domain.Events.EducationProgram;
using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationProgram: AggregateRoot
{
    public required string Name { get; set; }

    public required string Code { get; set; }
    
    public required string Language { get; set; }
    
    public required string EducationForm { get; set; }
    
    public Guid FacultyId { get; set; }
    
    public int EducationLevelId { get; set; }

    public Faculty Faculty { get; set; } = null!;
    public EducationLevel EducationLevel { get; set; } = null!;
    
    public override void ChangeDeleteTime(DateTime? deleteTime)
    {
        if ((deleteTime == null && DeleteTime == null) || (deleteTime != null && DeleteTime != null)) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new ProgramDeleteTimeChangedDomainEvent(this));
    }
}