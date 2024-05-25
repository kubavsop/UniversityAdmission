using Admission.Dictionary.Domain.Events.NextEducationLevel;
using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class NextEducationLevel: AggregateRoot
{
    public Guid EducationDocumentTypeId { get; set; }
    public int EducationLevelId { get; set; }

    public EducationLevel EducationLevel { get; set; } = null!;
    public EducationDocumentType EducationDocumentType { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;
        
        var other = (NextEducationLevel)obj;
        
        return EducationDocumentTypeId == other.EducationDocumentTypeId 
               && EducationLevelId == other.EducationLevelId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(EducationDocumentTypeId, EducationLevelId);
    }
    
    public override void ChangeDeleteTime(DateTime? deleteTime)
    {
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new NextLevelDeleteTimeChangedDomainEvent(this));
    }
}