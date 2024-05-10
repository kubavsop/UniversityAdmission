using System.ComponentModel.DataAnnotations;
using Admission.Document.Application.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Admission.Document.Application.DTOs.Requests;

public sealed class CreateScanDto
{
    [Required]
    [AllowedFileExtensions]
    [FileMaxSize(1024 * 1024 * 10)]
    public required IFormFile File { get; set; }
}