using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Faculty;

public sealed class FacultyDeleteTimeChangedIntegrationEventHandler : IIntegrationEventHandler<FacultyDeleteTimeChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public FacultyDeleteTimeChangedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(FacultyDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (faculty == null) return;

        faculty.DeleteTime = notification.DeleteTime;

        await _context.SaveChangesAsync(cancellationToken);
    }
}