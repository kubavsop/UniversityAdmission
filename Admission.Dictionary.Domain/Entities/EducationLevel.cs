﻿using Admission.Dictionary.Domain.Events.EducationLevel;
using Admission.Domain.Common.Entities;

namespace Admission.Dictionary.Domain.Entities;

public sealed class EducationLevel: AggregateRoot
{
    public int ExternalId { get; set; }
    public required string Name { get; set; }
    public ICollection<EducationDocumentType> DocumentTypes { get; } = new List<EducationDocumentType>();
    public ICollection<NextEducationLevel> NextEducationLevels { get; } = new List<NextEducationLevel>();

    public override void ChangeDeleteTime(DateTime? deleteTime)
    {
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new EducationLevelDeleteTimeChangedDomainEvent(this));
    }

    public void ChangeName(string name)
    {
        if (Name == name) return;
        Name = name;
        AddDomainEvent(new LevelNameChangedDomainEvent(this));
    }
}