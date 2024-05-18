using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramCodeChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramCodeChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramCodeChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramCodeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        program.Code = notification.Code;
        await _context.SaveChangesAsync(cancellationToken);
    }
}