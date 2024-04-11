using Admission.Application.Common.Messaging;

namespace Admission.Infrastructure.Common.Services;

public interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}