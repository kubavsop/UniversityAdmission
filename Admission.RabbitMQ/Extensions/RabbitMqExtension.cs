using Admission.Application.Common.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.RabbitMQ.BackgroundServices;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services;
using Admission.RabbitMQ.Services.Impl;
using Admission.RabbitMQ.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Admission.RabbitMQ.Extensions;

public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMqConnection(this IServiceCollection services,
        IConfiguration configuration)
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

    public static IServiceCollection AddRpcConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RpcConsumerQueueNameOptions>(configuration.GetSection("RpcConsumerQueueName"));
        services.AddScoped<IRpcConsumer, RpcConsumer>();
        services.AddHostedService<RpcConsumerBackgroundService>();

        return services;
    }

    public static IServiceCollection AddRpcDictionaryClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RpcClientQueueNameOptions>(configuration.GetSection("RpcClientQueueName"));
        services.AddSingleton<IRpcDictionaryClient, RpcClient>();
        return services;
    }

}