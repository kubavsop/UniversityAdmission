using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramEducationLevelChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramEducationLevelChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramEducationLevelChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramEducationLevelChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;
        
        if (!await _context.EducationLevels.AnyAsync(l => l.ExternalId == notification.EducationLevelId,
                cancellationToken))
        {
            await _context.EducationLevels.AddAsync(new Domain.Entities.EducationLevel
            {
                ExternalId = notification.EducationLevelId,
                Name = notification.EducationLevelName
            }, cancellationToken);
        }

        program.EducationLevelId = notification.EducationLevelId;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}