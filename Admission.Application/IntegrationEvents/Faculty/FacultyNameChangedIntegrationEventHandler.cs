using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Faculty;

public sealed class FacultyNameChangedIntegrationEventHandler: IIntegrationEventHandler<FacultyNameChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public FacultyNameChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(FacultyNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (faculty == null) return;

        faculty.Name = notification.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}