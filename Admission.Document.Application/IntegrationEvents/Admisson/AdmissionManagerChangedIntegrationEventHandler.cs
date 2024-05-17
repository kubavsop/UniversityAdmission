using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Admisson;

public sealed class AdmissionManagerChangedIntegrationEventHandler : IIntegrationEventHandler<AdmissionManagerChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public AdmissionManagerChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionManagerChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa => sa.Id == notification.Id, cancellationToken: cancellationToken);
        if (studentAdmission == null) return;

        studentAdmission.ManagerId = notification.ManagerId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}