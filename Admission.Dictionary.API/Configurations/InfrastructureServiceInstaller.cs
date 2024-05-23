using Admission.API.Common.ServiceInstaller;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Infrastructure;
using Admission.Dictionary.Infrastructure.Options;
using Admission.Dictionary.Infrastructure.Services;
using Admission.Infrastructure.Common.Interceptors;
using Admission.JWT;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.Extensions;
using Admission.OutboxMessages.Interceptors;
using Admission.RabbitMQ.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.API.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddConsumer()
            .AddJwtAuthentication()
            .AddRabbitMqConnection(configuration)
            .AddProducer()
            .AddRpcConsumer(configuration)
            .AddOutboxMessages();
        
        services.Configure<ApiOptions>(configuration.GetSection("Api"));
        services.AddHttpClient<IExternalDictionaryService, ExternalDictionaryService>();
        
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IDictionaryDbContext>(provider => provider.GetRequiredService<DictionaryDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<DictionaryDbContext>());
            
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DictionaryDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}