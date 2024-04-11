namespace Admission.Application.Common.Messaging;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent);
}