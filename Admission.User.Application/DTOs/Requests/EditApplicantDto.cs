using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.ValidationAttributes;
using Admission.Domain.Common.Enums;

namespace Admission.User.Application.DTOs.Requests;

public class EditApplicantDto
{
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
}