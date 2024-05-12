using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;

namespace Admission.Application.IntegrationEvents.Applicant;

public sealed class ApplicantCreatedIntegrationEventHandler: IIntegrationEventHandler<ApplicantCreatedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public ApplicantCreatedIntegrationEventHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApplicantCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _context.Applicants.AddAsync(new Domain.Entities.Applicant
        {
            Id = notification.Id,
            Email = notification.Email,
            FullName = notification.FullName
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}