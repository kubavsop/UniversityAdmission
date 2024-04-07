using System.ComponentModel.DataAnnotations;

namespace Admission.Application.Common.ValidationAttributes;

public class BirthdayAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is DateTime dateTime && dateTime < DateTime.UtcNow
            ? ValidationResult.Success
            : new ValidationResult("Birth date can't be later than today");
}