using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;

namespace Admission.Document.Application.IntegrationEvents.Manager;

public sealed class ManagerCreatedIntegrationEventHandler: IIntegrationEventHandler<ManagerCreatedIntegrationEvent>, IIntegrationEvent
{
    private readonly IDocumentDbContext _context;

    public ManagerCreatedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _context.Managers.AddAsync(new Domain.Entities.Manager
        {
            Id = notification.Id,
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}