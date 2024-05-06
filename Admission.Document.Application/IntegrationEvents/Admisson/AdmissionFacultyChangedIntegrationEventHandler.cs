using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Admisson;

public sealed class AdmissionFacultyChangedIntegrationEventHandler: IIntegrationEventHandler<AdmissionFacultyChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public AdmissionFacultyChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionFacultyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa => sa.Id == notification.Id, cancellationToken: cancellationToken);
        if (studentAdmission == null) return;
        
        studentAdmission.FirstPriorityFacultyId = notification.FirstPriorityFacultyId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}