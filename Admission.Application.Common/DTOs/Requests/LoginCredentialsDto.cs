using System.ComponentModel.DataAnnotations;

namespace Admission.Application.Common.DTOs.Requests;

public sealed class LoginCredentialsDto
{
    [Required]
    [EmailAddress]
    [MaxLength(1000)]
    public required string Email { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public required string Password { get; set; }
}