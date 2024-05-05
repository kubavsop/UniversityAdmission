using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Admission.Document.Application.DTOs.Requests;

public sealed class CreateScanDto
{
    [Required]
    public required IFormFile File { get; set; }
}