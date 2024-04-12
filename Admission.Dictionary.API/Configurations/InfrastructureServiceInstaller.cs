using Admission.API.Common.ServiceInstaller;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Infrastructure;
using Admission.Infrastructure.Common.Extensions;
using Admission.Infrastructure.Common.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.API.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication();
        
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IDictionaryDbContext>(provider => provider.GetRequiredService<DictionaryDbContext>());
        services.AddScoped<IDictionaryService, DictionaryService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DictionaryDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}