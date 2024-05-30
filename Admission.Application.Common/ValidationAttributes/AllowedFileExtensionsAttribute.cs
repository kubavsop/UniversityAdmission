using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace Admission.Application.Common.ValidationAttributes;

public sealed class AllowedFileExtensionsAttribute: ValidationAttribute
{
    private static readonly string[] Extensions = ContentTypeMappings.TypeMappings.Keys.ToArray();
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is FormFile formFile && Extensions.Contains(Path.GetExtension(formFile.FileName))
            ? ValidationResult.Success
            : new ValidationResult($"Invalid extension. Allowed extensions are: {string.Join(", ", Extensions)}");
}