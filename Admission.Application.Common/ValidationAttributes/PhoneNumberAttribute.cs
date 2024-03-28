using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Admission.Application.Common.ValidationAttributes;

public sealed partial class PhoneNumberAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => 
        value is string stringValue && PhoneRegex().IsMatch(stringValue)
            ? ValidationResult.Success
            : new ValidationResult("Phone number is not a valid");

    [GeneratedRegex(@"^(\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}|((\+7|8)\d{3}\d{3}\d{2}\d{2}))$")]
    private static partial Regex PhoneRegex();
}