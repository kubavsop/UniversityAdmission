using Admission.Infrastructure.Common.Messaging.Options;
using Admission.Infrastructure.Common.Messaging.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Admission.Infrastructure.Common.Extensions;

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
}