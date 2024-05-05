using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class AdmissionProgram: AggregateRoot
{
    public int Priority { get; set; }
    public Guid StudentAdmissionId { get; set; }
    public Guid EducationProgramId { get; set; }

    public StudentAdmission StudentAdmission { get; set; } = null!;
    public EducationProgram EducationProgram { get; set; } = null!;
}