using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.NextEducationLevel;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.NextEducationLevel;

public sealed class NextLevelDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<NextLevelDeleteTimeChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public NextLevelDeleteTimeChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(NextLevelDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var level = await _context.NextEducationLevels.FirstOrDefaultAsync(nel => nel.Id == notification.Id, cancellationToken: cancellationToken);
        if (level == null) return;
        
        level.DeleteTime = notification.DeleteTime;
        await _context.SaveChangesAsync(cancellationToken);
    }
}