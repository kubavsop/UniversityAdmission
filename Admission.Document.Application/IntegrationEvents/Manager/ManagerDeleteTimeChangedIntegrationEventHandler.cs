using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Manager;

public sealed class ManagerDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<ManagerDeleteTimeChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public ManagerDeleteTimeChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (manager == null) return;

        manager.DeleteTime = notification.DeleteTime;

        await _context.SaveChangesAsync(cancellationToken);
    }
}