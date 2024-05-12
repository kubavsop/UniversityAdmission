using Admission.Application.Common.Mapping;
using Admission.Domain.Common.Enums;
using Admission.Domain.Entities;
using AutoMapper;

namespace Admission.Application.DTOs.Responses;

public sealed class StudentAdmissionDto: IMapFrom<StudentAdmission>
{
    public Guid Id { get; init; }
    public DateTime CreateTime { get; init; }
    public AdmissionStatus Status { get; init; }
    public bool ExistManager { get; init; }
    public DateTime? ModifiedTime { get; init; }
    public IEnumerable<AdmissionProgramDto> AdmissionPrograms { get; init; } = new List<AdmissionProgramDto>();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StudentAdmission, StudentAdmissionDto>()
            .ForMember(dest => dest.ExistManager,
                opt => opt.MapFrom(src => src.ManagerId != null));
    }
}