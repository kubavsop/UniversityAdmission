using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;
using Admission.User.Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.IntegrationEvents.Faculty;

public sealed class FacultyDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<FacultyDeleteTimeChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public FacultyDeleteTimeChangedIntegrationEventHandler(IUserDbContext context)
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