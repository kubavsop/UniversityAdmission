using Admission.RabbitMQ.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Admission.RabbitMQ.Setups;

public sealed class IntegrationConsumerQueueNameOptionsSetup: IConfigureOptions<IntegrationConsumerQueueNameOptions>
{
    private const string SectionName = "IntegrationConsumerQueueName";
    private readonly IConfiguration _configuration;

    public IntegrationConsumerQueueNameOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(IntegrationConsumerQueueNameOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}