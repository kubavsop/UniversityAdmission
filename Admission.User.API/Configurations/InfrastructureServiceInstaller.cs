using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Messaging;
using Admission.Infrastructure.Common.Context;
using Admission.Infrastructure.Common.Interceptors;
using Admission.Infrastructure.Common.Messaging;
using Admission.Infrastructure.Common.Messaging.Settings;
using Admission.Infrastructure.Common.Messaging.Settings.Setups;
using Admission.Infrastructure.Common.Outbox.BackgroundServices;
using Admission.User.Application.Context;
using Admission.User.Application.Services;
using Admission.User.Domain.Entities;
using Admission.User.Infrastructure;
using Admission.User.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.API.Configurations;

public class UserDbServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<IntegrationQueuesOptionsSetup>();
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        services.AddScoped<IUserDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        
        services
            .AddIdentityCore<AdmissionUser>()
            .AddRoles<AdmissionRole>()
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        
        services.AddHostedService<OutboxMessageProcessorService>();
        
        services.AddScoped<IProcessOutboxMessageService, ProcessOutboxMessageService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<UserDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}