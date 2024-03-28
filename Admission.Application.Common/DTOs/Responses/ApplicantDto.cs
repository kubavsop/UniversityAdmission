using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.ValidationAttributes;
using Admission.Domain.Common.Enums;

namespace Admission.Application.Common.DTOs.Responses;

public sealed class ApplicantDto
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

    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public required string Password { get; set; }
    
    [Birthday]
    public DateOnly? Birthday { get; set; }

    public Gender? Gender { get; set; }
    
    [MinLength(1)]
    [MaxLength(1000)]
    public string? Citizenship { get; set; }

    [PhoneNumber]
    public string? PhoneNumber { get; set; }
}