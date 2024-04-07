using Admission.Domain.Common.Enums;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.User.Infrastructure;

public static class InfrastructureConfiguration
{
    public static async Task AddAutoMigrationAsync(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            await context.Database.MigrateAsync();
        }
    }

    public static async Task EnsureRoleCreatedAsync(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AdmissionRole>>();

            var roles = GetDefaultAdmissionRoles();

            foreach (var role in roles)
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AdmissionRole { Type = role, Name = roleName });
                }
            }
        }
    }
    
    private static IEnumerable<RoleType> GetDefaultAdmissionRoles() =>
        Enum.GetValues<RoleType>();
}