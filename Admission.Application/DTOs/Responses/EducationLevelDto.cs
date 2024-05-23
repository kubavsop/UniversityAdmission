using Admission.Application.Common.Mapping;
using Admission.Domain.Entities;
using AutoMapper;

namespace Admission.Application.DTOs.Responses;

public sealed class EducationLevelDto: IMapFrom<EducationLevel>
{
    public int Id { get; init; }
    public required string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EducationLevel, EducationLevelDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));
    }
}