using System.ComponentModel.DataAnnotations;

namespace Admission.Document.Application.DTOs.Requests;

public sealed class CreateEducationDocumentDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    public required string Name { get; set; }
    
    [Required]
    public required Guid EducationDocumentTypeId { get; set; }
}