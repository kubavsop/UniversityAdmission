using Admission.API.Common.ServiceInstaller;
using Admission.Infrastructure.Common.BackgroundServices;
using Admission.Infrastructure.Common.Extensions;
using Admission.Infrastructure.Common.Messaging.Options;
using Admission.Infrastructure.Common.Messaging.Setups;
using Admission.Infrastructure.Common.Services;
using Admission.Infrastructure.Common.Services.Impl;
using Admission.Notification.Application.Services;
using Admission.Notification.Infrastructure.Options;
using Admission.Notification.Infrastructure.Services;

namespace Admission.Notification.API.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMqConnection(configuration);
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        services.ConfigureOptions<IntegrationConsumerQueueNameOptionsSetup>();
        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();
    }
}