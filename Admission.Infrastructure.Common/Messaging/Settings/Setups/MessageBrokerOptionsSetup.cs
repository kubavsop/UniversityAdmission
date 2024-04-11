using Admission.Infrastructure.Common.Messaging.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Admission.Infrastructure.Common.Messaging.Settings.Setups;

public sealed class MessageBrokerOptionsSetup: IConfigureOptions<MessageBrokerOptions>
{
    private const string SectionName = "MessageBroker";
    private readonly IConfiguration _configuration;

    public MessageBrokerOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
  
    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}