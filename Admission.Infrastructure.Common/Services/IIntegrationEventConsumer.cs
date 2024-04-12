using Admission.Application.Common.Messaging.IntegrationEvent;


namespace Admission.Infrastructure.Common.Services;

public interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}