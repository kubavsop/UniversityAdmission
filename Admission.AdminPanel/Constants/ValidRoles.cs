using Admission.Domain.Common.Enums;

namespace Admission.AdminPanel.Constants;

public static class ValidRoles
{
    public static IEnumerable<string> Roles { get; } = new[] { RoleType.Manager.ToString(), RoleType.SeniorManager.ToString(), RoleType.Admin.ToString() };

    public static RoleType[] EnumRoles { get; } =
        [RoleType.Manager, RoleType.SeniorManager, RoleType.Admin];
}