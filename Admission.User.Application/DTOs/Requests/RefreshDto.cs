using System.ComponentModel.DataAnnotations;

namespace Admission.User.Application.DTOs.Requests;

public sealed class RefreshDto
{
    [Required]
    public required string RefreshToken { get; set; }
    
    [Required]
    public required string AccessToken { get; set; }
}