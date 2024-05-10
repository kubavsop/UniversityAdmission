using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Admission.Document.Application.ValidationAttributes;

public sealed class AllowedFileExtensionsAttribute: ValidationAttribute
{
    private static string[] _extensions = [".jpe", ".jpeg", ".jpg", ".pdf", ".png"];


    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is FormFile formFile && _extensions.Contains(Path.GetExtension(formFile.FileName))
            ? ValidationResult.Success
            : new ValidationResult("Invalid extension");
}