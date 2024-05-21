using Admission.Application.Common.Exceptions;
using Admission.Domain.Common.Enums;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using Admission.User.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

    public static async Task EnsureAdminCreatedAsync(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var adminSettings = serviceProvider.GetRequiredService<IOptions<AdminOptions>>().Value;
            var userManager = serviceProvider.GetRequiredService<UserManager<AdmissionUser>>();
            var context = serviceProvider.GetRequiredService<IUserDbContext>();
            
            
            var defaultRole = RoleType.Applicant.ToString();
            var user = new AdmissionUser
            {
                Email = adminSettings.Email,
                FullName = adminSettings.Name
            };

            var result = await userManager.CreateAsync(user, adminSettings.Password);

            if (!result.Succeeded) return;
            
            await userManager.AddToRolesAsync(user, [defaultRole, RoleType.Admin.ToString()]);
            await context.Applicants.AddAsync(Applicant.Create(user));
            await context.Managers.AddAsync(Manager.Create(user));
            await context.SaveChangesAsync();
        }
    }
    
    private static IEnumerable<RoleType> GetDefaultAdmissionRoles() =>
        Enum.GetValues<RoleType>();
}