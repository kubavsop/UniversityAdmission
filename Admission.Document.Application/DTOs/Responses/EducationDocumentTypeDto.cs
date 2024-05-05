using Admission.Application.Common.Mapping;
using Admission.Document.Domain.Entities;
using AutoMapper;

namespace Admission.Document.Application.DTOs.Responses;

public class EducationDocumentTypeDto: IMapFrom<EducationDocumentType>
{
    public required string Name { get; set; }
    public EducationLevelDto EducationLevel { get; set; } = null!;

    public ICollection<EducationLevelDto> NextEducationLevels { get; } = new List<EducationLevelDto>();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EducationDocumentType, EducationDocumentTypeDto>()
            .ForMember(dest => dest.NextEducationLevels,
                opt => opt.MapFrom(src => src.NextEducationLevels
                    .Where(nel => !nel.EducationLevel.DeleteTime.HasValue)
                    .Select(nel => nel.EducationLevel)));
    }
}