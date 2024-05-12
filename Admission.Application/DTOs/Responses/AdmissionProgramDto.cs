using Admission.Application.Common.Mapping;
using Admission.Domain.Entities;

namespace Admission.Application.DTOs.Responses;

public sealed class AdmissionProgramDto: IMapFrom<AdmissionProgram>
{
    public Guid Id { get; init; }
    public DateTime CreateTime { get; init; }
    public int Priority { get; init; }
    public EducationProgramDto EducationProgram { get; init; } = null!;
}