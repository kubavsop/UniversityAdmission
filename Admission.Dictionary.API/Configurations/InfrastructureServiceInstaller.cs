using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Messaging.IntegrationEvent;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Application.Services.Impl;
using Admission.Dictionary.Infrastructure;
using Admission.Dictionary.Infrastructure.Options;
using Admission.Dictionary.Infrastructure.Services;
using Admission.Infrastructure.Common.BackgroundServices;
using Admission.Infrastructure.Common.Context;
using Admission.Infrastructure.Common.Extensions;
using Admission.Infrastructure.Common.Interceptors;
using Admission.Infrastructure.Common.Messaging.Setups;
using Admission.Infrastructure.Common.Services;
using Admission.Infrastructure.Common.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.API.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddJwtAuthentication()
            .AddRabbitMqConnection(configuration);
        
        services.ConfigureOptions<IntegrationQueuesOptionsSetup>();
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        
        services.Configure<ApiOptions>(configuration.GetSection("Api"));
        services.AddHttpClient<IExternalDictionaryService, ExternalDictionaryService>();
        
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IDictionaryDbContext>(provider => provider.GetRequiredService<DictionaryDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<DictionaryDbContext>());
        
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddHostedService<OutboxMessageProcessorService>();
        services.AddScoped<IProcessOutboxMessageService, ProcessOutboxMessageService>();
            
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