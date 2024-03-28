using System.ComponentModel.DataAnnotations;

namespace Admission.Application.Common.DTOs.Responses;

public sealed class TokenPairDto
{
    [Required]
    public required string AccessToken { get; set; }
    [Required]
    public required string RefreshToken { get; set; }
}