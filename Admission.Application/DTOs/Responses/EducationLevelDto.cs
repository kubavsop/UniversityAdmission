using Admission.Application.Common.Mapping;
using Admission.Domain.Entities;

namespace Admission.Application.DTOs.Responses;

public sealed class EducationLevelDto: IMapFrom<EducationLevel>
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
}