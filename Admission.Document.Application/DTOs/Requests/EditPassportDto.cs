using System.ComponentModel.DataAnnotations;
using Admission.Document.Application.ValidationAttributes;

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
    [DateIssued]
    public required DateTime DateIssued 
    {
        get => _dateIssued;
        set => _dateIssued = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    
    private DateTime _dateIssued;
}