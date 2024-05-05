using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class NextEducationLevel: BaseEntity
{
    public Guid ApplicantId { get; set; }
    public int EducationLevelId { get; set; }

    public EducationLevel EducationLevel { get; set; } = null!;
    public Applicant Applicant { get; set; } = null!;
}