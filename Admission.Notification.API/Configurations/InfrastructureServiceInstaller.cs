﻿using Admission.API.Common.ServiceInstaller;
using Admission.Notification.Application.Services;
using Admission.Notification.Infrastructure.Options;
using Admission.Notification.Infrastructure.Services;
using Admission.RabbitMQ.Extensions;

namespace Admission.Notification.API.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRabbitMqConnection(configuration)
            .AddConsumer();
        
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailService, EmailService>();
    }
}