using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationLevel;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationLevel;

public sealed class LevelNameChangedIntegrationEventHandler : IIntegrationEventHandler<LevelNameChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public LevelNameChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LevelNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var level = await _context.EducationLevels.FirstOrDefaultAsync(t => t.ExternalId == notification.ExternalId,
            cancellationToken);
        if (level == null) return;

        level.Name = notification.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}