using Admission.API.Common.ServiceInstaller;
using Admission.Infrastructure.Common.Messaging.Settings.Options;
using Admission.Infrastructure.Common.Messaging.Settings.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Admission.API.Common.Configurations;

public sealed class RabbitMqServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
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
    }
}