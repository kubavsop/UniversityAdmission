using MediatR;

namespace Admission.DTOs.IntegrationEvents;

public interface IIntegrationEventHandler<in TIntegrationEvent>: INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent: IIntegrationEvent
{
}