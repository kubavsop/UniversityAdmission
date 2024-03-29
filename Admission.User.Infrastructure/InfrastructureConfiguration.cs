using Admission.Infrastructure.Common.Interceptors;
using Admission.User.Application.Services;
using Admission.User.Domain.Entities;
using Admission.User.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.User.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<AdmissionUser>()
            .AddRoles<AdmissionRole>()
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<AuditableEntityInterceptor>();
        AddUserDbContext(services, configuration);
        return services;
    }
    
    private static void AddUserDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<UserDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
    
    public static void AddAutoMigration(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            context.Database.Migrate();
        }
    }
}