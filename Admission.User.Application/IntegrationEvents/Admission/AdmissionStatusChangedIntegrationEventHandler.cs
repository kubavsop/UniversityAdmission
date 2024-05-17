using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Admission.User.Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.IntegrationEvents.Admission;

public sealed class
    AdmissionStatusChangedIntegrationEventHandler : IIntegrationEventHandler<AdmissionStatusChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public AdmissionStatusChangedIntegrationEventHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionStatusChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa => sa.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (studentAdmission == null) return;

        studentAdmission.Status = notification.Status;

        await _context.SaveChangesAsync(cancellationToken);
    }
}