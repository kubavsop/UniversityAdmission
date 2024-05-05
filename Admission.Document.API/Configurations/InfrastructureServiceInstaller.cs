using Admission.API.Common.ServiceInstaller;
using Admission.Document.Application.Context;
using Admission.Document.Domain.Enums;
using Admission.Document.Infrastructure;
using Admission.Infrastructure.Common.Interceptors;
using Admission.JWT;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.Extensions;
using Admission.OutboxMessages.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.API.Configurations;

public class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddJwtAuthentication()
            .AddOutboxMessages();
        
        
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IDocumentDbContext>(provider => provider.GetRequiredService<DocumentDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<DocumentDbContext>());
            
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DocumentDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}