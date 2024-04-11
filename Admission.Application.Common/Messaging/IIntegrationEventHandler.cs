using MediatR;

namespace Admission.Application.Common.Messaging;

public interface IIntegrationEventHandler<in TIntegrationEvent>: INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent: IIntegrationEvent
{
}