using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Admission.Infrastructure.Common.Messaging.Setups;

public sealed class IntegrationConsumerQueueNameOptionsSetup: IConfigureOptions<IntegrationConsumerQueueNameOptionsSetup>
{
    private const string SectionName = "IntegrationConsumerQueueName";
    private readonly IConfiguration _configuration;

    public IntegrationConsumerQueueNameOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(IntegrationConsumerQueueNameOptionsSetup options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}