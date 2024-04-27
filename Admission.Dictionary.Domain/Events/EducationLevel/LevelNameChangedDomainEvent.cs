using Admission.Domain.Common.Events;

namespace Admission.Dictionary.Domain.Events.EducationLevel;

public class LevelNameChangedDomainEvent: IDomainEvent
{
    public LevelNameChangedDomainEvent()
    {
    }

    internal LevelNameChangedDomainEvent(Entities.EducationLevel educationLevel)
    {
        Id = educationLevel.Id;
        ExternalId = educationLevel.ExternalId;
        Name = educationLevel.Name;
    }
    
    public Guid Id { get; init; }
    public int ExternalId { get; init; }
    public string Name { get; init; }
    
}