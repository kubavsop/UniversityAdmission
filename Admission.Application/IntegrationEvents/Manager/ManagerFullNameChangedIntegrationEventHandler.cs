using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Manager;

public sealed class ManagerFullNameChangedIntegrationEventHandler: IIntegrationEventHandler<ManagerFullNameChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ManagerFullNameChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerFullNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == notification.Id, cancellationToken: cancellationToken);
        if (manager == null) return;

        manager.FullName = notification.FullName;

        await _context.SaveChangesAsync(cancellationToken);
    }
}