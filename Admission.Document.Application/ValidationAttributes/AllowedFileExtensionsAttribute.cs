using System.ComponentModel.DataAnnotations;
using Admission.Document.Application.Constants;
using Microsoft.AspNetCore.Http;

namespace Admission.Document.Application.ValidationAttributes;

public sealed class AllowedFileExtensionsAttribute: ValidationAttribute
{
    private static string[] _extensions = ContentTypeMappings.TypeMappings.Keys.ToArray();
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is FormFile formFile && _extensions.Contains(Path.GetExtension(formFile.FileName))
            ? ValidationResult.Success
            : new ValidationResult($"Invalid extension. Allowed extensions are: {string.Join(", ", _extensions)}");
}