using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramNameChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramNameChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramNameChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        program.Name = notification.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}