using Admission.Domain.Common.Entities;
using Admission.Domain.Events.Admission;

namespace Admission.Domain.Entities;

public sealed class AdmissionProgram : AggregateRoot
{
    public int Priority { get; private set; }
    public Guid StudentAdmissionId { get; private set; }
    public Guid EducationProgramId { get; private set; }

    public StudentAdmission StudentAdmission { get; private set; } = null!;
    public EducationProgram EducationProgram { get; private set; } = null!;

    public static AdmissionProgram Create(
        Guid studentAdmissionId,
        Guid educationProgramId,
        int priority)
    {
        var program = new AdmissionProgram
        {
            StudentAdmissionId = studentAdmissionId,
            EducationProgramId = educationProgramId,
            Priority = priority
        };

        return program;
    }

    public void ChangePriority(int priority, bool firstPriority)
    {
        if (Priority == priority) return;

        Priority = priority;
        if (firstPriority)
        {
            AddDomainEvent(new AdmissionFacultyChangedDomainEvent(StudentAdmissionId, EducationProgram.FacultyId,
                EducationProgram.Faculty.Name));
        }
    }
}