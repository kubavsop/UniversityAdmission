using RabbitMQ.Client;

namespace Admission.Notification.Infrastructure;

public sealed class EmailNotificationBackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IModel _channel;
}