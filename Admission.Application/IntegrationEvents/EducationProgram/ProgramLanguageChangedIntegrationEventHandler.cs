using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramLanguageChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramLanguageChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramLanguageChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramLanguageChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        program.Language = notification.Language;
        await _context.SaveChangesAsync(cancellationToken);
    }
}