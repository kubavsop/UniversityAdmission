using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationProgram;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.EducationProgram;

public sealed class ProgramFacultyChangedIntegrationEventHandler: IIntegrationEventHandler<ProgramFacultyChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ProgramFacultyChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProgramFacultyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var program = await _context.EducationPrograms.FirstOrDefaultAsync(p => p.Id == notification.Id, cancellationToken: cancellationToken);
        if (program == null) return;

        if (!await _context.Faculties.AnyAsync(f => f.Id == notification.FacultyId,
                cancellationToken: cancellationToken))
        {
            await _context.Faculties.AddAsync(new Domain.Entities.Faculty
            {
                Id = notification.Id,
                Name = notification.FacultyName
            });
        }

        program.FacultyId = notification.FacultyId;
        await _context.SaveChangesAsync(cancellationToken);
    }
}