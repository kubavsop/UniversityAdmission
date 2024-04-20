namespace Admission.Application.Common.Messaging.IntegrationEvent;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent);
}