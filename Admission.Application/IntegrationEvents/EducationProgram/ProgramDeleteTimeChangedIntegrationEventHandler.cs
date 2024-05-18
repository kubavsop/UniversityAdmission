using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramDeleteTimeChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramDeleteTimeChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        program.DeleteTime = notification.DeleteTime;
        await _context.SaveChangesAsync(cancellationToken);
    }
}