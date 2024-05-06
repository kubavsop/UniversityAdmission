using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Admission.User.Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.IntegrationEvents.Admission;

public sealed class
    AdmissionFacultyChangedIntegrationEventHandler : IIntegrationEventHandler<AdmissionFacultyChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public AdmissionFacultyChangedIntegrationEventHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionFacultyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa => sa.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (studentAdmission == null) return;

        studentAdmission.FirstPriorityFacultyId = notification.FirstPriorityFacultyId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}