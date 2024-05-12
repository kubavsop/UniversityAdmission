using Admission.Application.Common.Mapping;
using Admission.Domain.Entities;

namespace Admission.Application.DTOs.Responses;

public sealed class FacultyDto: IMapFrom<Faculty>
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
}