using System.ComponentModel.DataAnnotations;

namespace Admission.Dictionary.Application.DTOs.Responses;

public sealed class PageInfoDto
{
    [Required]
    public required int Size { get; set; }
    
    [Required]
    public required int Count { get; set; }
    
    [Required]
    public required int Current { get; set; }
}