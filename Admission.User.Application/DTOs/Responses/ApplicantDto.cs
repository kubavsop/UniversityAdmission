using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Application.Common.ValidationAttributes;
using Admission.Domain.Common.Enums;
using Admission.User.Domain.Entities;
using AutoMapper;

namespace Admission.User.Application.DTOs.Responses;

public sealed class ApplicantDto: IMapFrom<Applicant>
{
    [Required]
    public required Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public required string FullName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(1000)]
    public required string Email { get; set; }
    
    [Birthday]
    public DateTime? Birthday { get; set; }

    public Gender? Gender { get; set; }
    
    [MinLength(1)]
    [MaxLength(1000)]
    public string? Citizenship { get; set; }

    [PhoneNumber]
    public string? PhoneNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Applicant, ApplicantDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.User!.FullName))
            .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.User!.Email));
    }
}