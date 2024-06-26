﻿using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Domain.Entities;
using AutoMapper;

namespace Admission.Dictionary.Application.DTOs.Responses;

public sealed class EducationLevelDto: IMapFrom<EducationLevel>
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EducationLevel, EducationLevelDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));
    }
}