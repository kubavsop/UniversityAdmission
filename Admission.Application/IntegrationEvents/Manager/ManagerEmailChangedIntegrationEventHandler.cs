using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Manager;

public sealed class ManagerEmailChangedIntegrationEventHandler: IIntegrationEventHandler<ManagerEmailChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ManagerEmailChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerEmailChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == notification.Id, cancellationToken: cancellationToken);
        if (manager == null) return;

        manager.Email = notification.Email;

        await _context.SaveChangesAsync(cancellationToken);
    }
}