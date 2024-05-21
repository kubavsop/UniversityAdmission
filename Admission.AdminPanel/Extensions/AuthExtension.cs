using System.Security.Claims;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.Base;

namespace Admission.AdminPanel.Extensions;

public static class AuthExtension
{
    public static T SetAuthRequest<T>(this ClaimsPrincipal user, T request) where T : AuthorizedRequest
    {
        var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var userRole = Enum.Parse<RoleType>(user.FindFirst(ClaimTypes.Role)!.Value);
        request.Id = userId;
        request.Role = userRole;
        return request;
    }
}