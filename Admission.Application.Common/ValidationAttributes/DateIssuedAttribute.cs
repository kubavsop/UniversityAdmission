using System.ComponentModel.DataAnnotations;

namespace Admission.Document.Application.ValidationAttributes;

public sealed class DateIssuedAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is DateTime dateTime && dateTime < DateTime.UtcNow
            ? ValidationResult.Success
            : new ValidationResult("Date issued can't be later than today");
}