using MediatR;

namespace IntegrationEvents;

public interface IIntegrationEventHandler<in TIntegrationEvent>: INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent: IIntegrationEvent
{
}