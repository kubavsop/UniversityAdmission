using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramEducationFormChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramEducationFormChangeIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramEducationFormChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramEducationFormChangeIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        program.EducationForm = notification.EducationForm;
        await _context.SaveChangesAsync(cancellationToken);
    }
}