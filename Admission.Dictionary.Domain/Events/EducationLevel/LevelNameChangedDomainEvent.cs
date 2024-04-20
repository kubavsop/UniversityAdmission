using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationLevel;

public class LevelNameChangedDomainEvent: IDomainEvent
{
    public LevelNameChangedDomainEvent()
    {
    }

    internal LevelNameChangedDomainEvent(Entities.EducationLevel educationLevel)
    {
        Id = educationLevel.ExternalId;
        Name = educationLevel.Name;
    }
    
    public int Id { get; init; }
    public string Name { get; init; }
}