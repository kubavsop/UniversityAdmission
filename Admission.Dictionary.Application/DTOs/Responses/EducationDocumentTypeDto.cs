using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Domain.Entities;
using AutoMapper;

namespace Admission.Dictionary.Application.DTOs.Responses;

public class EducationDocumentTypeDto: IMapFrom<EducationDocumentType>
{
    [Required]
    public required Guid Id { get; set; }

    [Required]
    public required DateTime CreateTime { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required EducationLevelDto EducationLevel { get; set; }

    [Required]
    public required List<EducationLevelDto> NextEducationLevels { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EducationDocumentType, EducationDocumentTypeDto>()
            .ForMember(dest => dest.NextEducationLevels,
                opt => opt.MapFrom(src => src.EducationLevels));
    }
}