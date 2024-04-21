namespace IntegrationEvents;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent, string routingKey = "default");
}