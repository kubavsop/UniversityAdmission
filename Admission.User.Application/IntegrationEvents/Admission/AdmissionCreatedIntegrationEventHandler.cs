using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.IntegrationEvents.Admission;

public sealed class AdmissionCreatedIntegrationEventHandler: IIntegrationEventHandler<AdmissionCreatedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public AdmissionCreatedIntegrationEventHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _context.StudentAdmissions.AddAsync(new StudentAdmission
        {
            Id = notification.Id,
            ManagerId = notification.ManagerId,
            Status = notification.Status,
            ApplicantId = notification.ApplicantId
        }, cancellationToken);
    }
}