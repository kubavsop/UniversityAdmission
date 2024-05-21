using System.Security.Claims;
using Admission.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Admission.AdminPanel.Attributes;

public sealed class AuthorizeRoleFilter: IAuthorizationFilter
{
    private readonly IEnumerable<RoleType> _roles;

    public AuthorizeRoleFilter(IEnumerable<RoleType> roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role);
        if (userRole == null || !Enum.TryParse(userRole.Value, false, out RoleType role))
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!_roles.Contains(role))
        {
            context.Result = new ForbidResult();
        }
    }
}