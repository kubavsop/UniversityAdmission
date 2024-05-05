using System.ComponentModel.DataAnnotations;

namespace Admission.Document.Application.DTOs.Requests;

public class EditPassportDto
{
    [Required]
    [Range(1000, 9999)] 
    public int Series { get; set; }

    [Required]
    [Range(100000, 999999)]
    public int Number { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(400)]
    public required string PlaceOfBirth { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(400)]
    public required string IssuedBy { get; set; }
    
    [Required]
    [MinLength(1)]
    [MaxLength(400)]
    public required DateTime DateIssued { get; set; }
}