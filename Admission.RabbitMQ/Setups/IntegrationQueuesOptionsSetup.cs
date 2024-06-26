﻿using Admission.RabbitMQ.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Admission.RabbitMQ.Setups;

public sealed class IntegrationQueuesOptionsSetup: IConfigureOptions<IntegrationQueuesOptions>
{
    private const string SectionName = "IntegrationQueues";
    private readonly IConfiguration _configuration;

    public IntegrationQueuesOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public void Configure(IntegrationQueuesOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}