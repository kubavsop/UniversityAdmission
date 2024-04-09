using Admission.Domain.Common.Events;
using Admission.User.Domain.Events;

namespace Admission.User.Application.Events.ApplicantCreated;

public sealed class ApplicantCreatedEventHandler: IDomainEventHandler<ApplicantCreatedDomainEvent>
{
    
    public Task Handle(ApplicantCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"PUBLISH notification with id={notification.Id}");
        
        
        return Task.CompletedTask;
    }
}