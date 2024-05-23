using Admission.API.Common.ServiceInstaller;
using Admission.Application.Context;
using Admission.Infrastructure;
using Admission.Infrastructure.Common.Interceptors;
using Admission.JWT;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.Extensions;
using Admission.OutboxMessages.Interceptors;
using Admission.RabbitMQ.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Admission.API.Configurations;

public class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRpcDictionaryClient(configuration)
            .AddRabbitMqConnection(configuration)
            .AddProducer()
            .AddConsumer()
            .AddJwtAuthentication()
            .AddOutboxMessages();
        
        
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IAdmissionDbContext>(provider => provider.GetRequiredService<AdmissionDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<AdmissionDbContext>());
            
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AdmissionDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}