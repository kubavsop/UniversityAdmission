﻿using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Domain.Entities;

namespace Admission.Dictionary.Application.DTOs;

public sealed class EducationLevelDto: IMapFrom<EducationLevel>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
}