using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Admisson;

public sealed class AdmissionStatusChangedIntegrationEventHandler : IIntegrationEventHandler<AdmissionStatusChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public AdmissionStatusChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionStatusChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa => sa.Id == notification.Id, cancellationToken: cancellationToken);
        if (studentAdmission == null) return;

        studentAdmission.Status = notification.Status;

        await _context.SaveChangesAsync(cancellationToken);
    }
}