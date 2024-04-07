using Microsoft.AspNetCore.Identity;

namespace Admission.Application.Common.Exceptions;

public sealed class IdentityException: Exception
{
    public List<IdentityError>? Errors { get; set; }
    
    public IdentityException(List<IdentityError> errors)
    {
        Errors = errors;
    }
}