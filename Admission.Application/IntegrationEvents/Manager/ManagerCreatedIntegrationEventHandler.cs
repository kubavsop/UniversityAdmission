using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;

namespace Admission.Application.IntegrationEvents.Manager;

public sealed class ManagerCreatedIntegrationEventHandler: IIntegrationEventHandler<ManagerCreatedIntegrationEvent>, IIntegrationEvent
{
    private readonly IAdmissionDbContext _context;

    public ManagerCreatedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _context.Managers.AddAsync(new Domain.Entities.Manager
        {
            Id = notification.Id,
            Email = notification.Email,
            FullName = notification.FullName
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}