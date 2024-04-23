using Admission.RabbitMQ.BackgroundService;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services;
using Admission.RabbitMQ.Services.Impl;
using Admission.RabbitMQ.Setups;
using IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Admission.RabbitMQ.Extensions;

public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMqConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerSettings = configuration.GetSection("MessageBroker").Get<MessageBrokerOptions>()!;
        var connectionFactory = new ConnectionFactory
        {
            HostName = messageBrokerSettings.HostName,
            Port = messageBrokerSettings.Port,
            UserName = messageBrokerSettings.UserName,
            Password = messageBrokerSettings.Password
        };

        services.ConfigureOptions<MessageBrokerOptionsSetup>();
        services.AddSingleton(connectionFactory.CreateConnection());
        return services;
    }

    public static IServiceCollection AddConsumer(this IServiceCollection services)
    {
        services.ConfigureOptions<IntegrationConsumerQueueNameOptionsSetup>();
        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        return services;
    }

    public static IServiceCollection AddProducer(this IServiceCollection services)
    {
        services.ConfigureOptions<IntegrationQueuesOptionsSetup>();
        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();
        return services;
    }
}