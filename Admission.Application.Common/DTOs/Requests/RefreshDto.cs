using System.ComponentModel.DataAnnotations;

namespace Admission.Application.Common.DTOs.Requests;

public sealed class RefreshDto
{
    [Required]
    public required string RefreshToken { get; set; }
}