namespace Admission.DTOs.IntegrationEvents;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent, string routingKey);
}