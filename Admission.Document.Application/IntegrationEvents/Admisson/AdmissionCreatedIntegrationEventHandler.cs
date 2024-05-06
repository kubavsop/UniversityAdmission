using Admission.Document.Application.Context;
using Admission.Document.Domain.Entities;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Admisson;

public sealed class AdmissionCreatedIntegrationEventHandler: IIntegrationEventHandler<AdmissionCreatedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public AdmissionCreatedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.FirstPriorityFacultyId.HasValue && !await _context.Faculties
                .AnyAsync(f => f.Id == notification.FirstPriorityFacultyId, cancellationToken))
        {
            await _context.Faculties.AddAsync(new Domain.Entities.Faculty
            {
                Id = notification.FirstPriorityFacultyId.Value
            }, cancellationToken);
        }

        await _context.StudentAdmissions.AddAsync(new StudentAdmission
        {
            Id = notification.Id,
            ManagerId = notification.ManagerId,
            Status = notification.Status,
            FirstPriorityFacultyId = notification.FirstPriorityFacultyId,
            ApplicantId = notification.ApplicantId
        }, cancellationToken);
    }
}