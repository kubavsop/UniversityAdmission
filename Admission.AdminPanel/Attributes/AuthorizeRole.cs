using Admission.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Attributes;

public sealed class AuthorizeRole : TypeFilterAttribute
{
    public AuthorizeRole(RoleType[] roles) : base(typeof(AuthorizeRoleFilter))
    {
        Arguments = new object[] { roles };
    }

    public AuthorizeRole(RoleType role)
        : this(new []{ role }) { }
}