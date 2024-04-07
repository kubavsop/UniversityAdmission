using System.ComponentModel.DataAnnotations;

namespace Admission.User.Application.DTOs.Requests;

public class EditPasswordDto
{
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public required string OldPassword { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public required string NewPassword { get; set; }
}