using Admission.DTOs.IntegrationEvents;

namespace Admission.RabbitMQ.Services;

public interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}