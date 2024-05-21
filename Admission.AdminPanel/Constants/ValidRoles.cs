﻿using Admission.Domain.Common.Enums;

namespace Admission.AdminPanel.Constants;

public static class ValidRoles
{
    public static string ManagerRole { get; } = RoleType.Manager.ToString();
    public static string SeniorManagerRole { get; } = RoleType.SeniorManager.ToString();
    public static string AdminRole { get; } = RoleType.Admin.ToString();
    public static IEnumerable<string> Roles { get; } = new[] { ManagerRole, SeniorManagerRole, AdminRole };
}