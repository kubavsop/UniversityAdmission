using MediatR;

namespace Admission.Application.Common.Messaging.IntegrationEvent;

public interface IIntegrationEventHandler<in TIntegrationEvent>: INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent: IIntegrationEvent
{
}