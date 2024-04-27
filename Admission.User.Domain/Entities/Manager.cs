﻿using Admission.Domain.Common.Entities;

namespace Admission.User.Domain.Entities;

public sealed class Manager: BaseEntity
{
    public AdmissionUser? User { get; set; }
    public Guid? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
    
    public ICollection<StudentAdmission> Admissions { get; } = new List<StudentAdmission>();
}