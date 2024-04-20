using MediatR;

namespace Admission.Domain.Common.Events;

public interface IDomainEventHandler<in TDomainEvent>: INotificationHandler<TDomainEvent>
    where TDomainEvent: IDomainEvent
{
}