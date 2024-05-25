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
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new ProgramDeleteTimeChangedDomainEvent(this));
    }
    
    public void ChangeName(string name)
    {
        if (Name == name) return;
        Name = name;
        AddDomainEvent(new ProgramNameChangedDomainEvent(this));
    }
    
    public void ChangeCode(string code)
    {
        if (Code == code) return;
        Code = code;
        AddDomainEvent(new ProgramCodeChangedDomainEvent(this));
    }

    public void ChangeLanguage(string language)
    {
        if (Language == language) return;
        Language = language;
        AddDomainEvent(new ProgramLanguageChangedDomainEvent(this));
    }

    public void ChangeEducationForm(string educationForm)
    {
        if (EducationForm == educationForm) return;
        EducationForm = educationForm;
        AddDomainEvent(new ProgramEducationFormChangeDomainEvent(this));
    }

    public void ChangeFaculty(Faculty faculty)
    {
        if (FacultyId == faculty.Id) return;
        FacultyId = faculty.Id;
        AddDomainEvent(new ProgramFacultyChangedDomainEvent(this, faculty));
    }

    public void ChangeEducationLevel(EducationLevel level)
    {
        if (EducationLevelId == level.ExternalId) return;
        EducationLevelId = level.ExternalId;
        AddDomainEvent(new ProgramEducationLevelChangedDomainEvent(this, level));
    }
}