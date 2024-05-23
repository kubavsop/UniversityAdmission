using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationLevel;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationLevel;

public sealed class LevelDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<LevelDeleteTimeChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public LevelDeleteTimeChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LevelDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var level = await _context.EducationLevels.FirstOrDefaultAsync(t => t.ExternalId == notification.ExternalId,
            cancellationToken);
        if (level == null) return;

        level.DeleteTime = notification.DeleteTime;

        await _context.SaveChangesAsync(cancellationToken);
    }
}