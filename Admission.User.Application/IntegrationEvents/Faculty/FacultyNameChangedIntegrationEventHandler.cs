using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Faculty;
using Admission.User.Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.IntegrationEvents.Faculty;

public sealed class FacultyNameChangedIntegrationEventHandler : IIntegrationEventHandler<FacultyNameChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public FacultyNameChangedIntegrationEventHandler(IUserDbContext context)
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