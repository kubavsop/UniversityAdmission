using Admission.Dictionary.Domain.Events.DocumentType;
using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationDocumentType: AggregateRoot
{
    public string Name { get; private set; }
    
    public int EducationLevelId { get; private set; }
    
    public EducationLevel EducationLevel { get; private set; } = null!;

    public ICollection<EducationLevel> EducationLevels { get; } = new List<EducationLevel>();
    public ICollection<NextEducationLevel> NextEducationLevels { get; } = new List<NextEducationLevel>();

    public EducationDocumentType(Guid id, string name, int educationLevelId)
    {
        Id = id;
        Name = name;
        EducationLevelId = educationLevelId;
    }
    
    public void ChangeName(string name)
    {
        if (Name == name) return;
        Name = name;
        AddDomainEvent(new DocumentNameChangedDomainEvent(this));
    }

    public override void ChangeDeleteTime(DateTime? deleteTime)
    {
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new DocumentDeleteTimeChangedDomainEvent(this));
    }

    public void ChangeEducationLevel(EducationLevel level)
    {
        if (EducationLevelId == level.ExternalId) return;

        EducationLevelId = level.ExternalId;
        AddDomainEvent(new DocumentEducationLevelChangedDomainEvent(this, level));
    }
}