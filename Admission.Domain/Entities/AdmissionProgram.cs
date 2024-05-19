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


    private AdmissionProgram()
    {
    }
    
    public static AdmissionProgram Create(
        Guid studentAdmissionId,
        EducationProgram educationProgram,
        int priority,
        bool isFirstPriority)
    {
        var program = new AdmissionProgram
        {
            StudentAdmissionId = studentAdmissionId,
            EducationProgramId = educationProgram.Id,
            EducationProgram = educationProgram,
            Priority = priority
        };
        
        if (isFirstPriority)
        {
            program.AddDomainEvent(new AdmissionFacultyChangedDomainEvent(studentAdmissionId, educationProgram.FacultyId,
                educationProgram.Faculty.Name));
        }

        return program;
    }

    public void ChangePriority(int priority, bool isFirstPriority)
    {
        if (Priority == priority) return;

        Priority = priority;
        if (isFirstPriority)
        {
            AddDomainEvent(new AdmissionFacultyChangedDomainEvent(StudentAdmissionId, EducationProgram.FacultyId,
                EducationProgram.Faculty.Name));
        }
    }
}