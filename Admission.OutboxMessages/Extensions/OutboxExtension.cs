using Admission.OutboxMessages.BackgroundServices;
using Admission.OutboxMessages.Interceptors;
using Admission.OutboxMessages.Services;
using Admission.OutboxMessages.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.OutboxMessages.Extensions;

public static class OutboxExtension
{
    public static IServiceCollection AddOutboxMessages(this IServiceCollection services)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddHostedService<OutboxMessageProcessorService>();
        services.AddScoped<IProcessOutboxMessageService, ProcessOutboxMessageService>();
        return services;
    }
}