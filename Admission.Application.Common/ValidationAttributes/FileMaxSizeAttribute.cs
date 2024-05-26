using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Admission.Application.Common.ValidationAttributes;

public sealed class FileMaxSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public FileMaxSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is FormFile formFile && formFile.Length <= _maxFileSize
            ? ValidationResult.Success
            : new ValidationResult($"Maximum allowed size is {_maxFileSize} bytes.");
}