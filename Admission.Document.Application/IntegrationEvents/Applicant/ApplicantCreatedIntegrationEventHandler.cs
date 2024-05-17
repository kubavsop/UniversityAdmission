using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;

namespace Admission.Document.Application.IntegrationEvents.Applicant;

public sealed class ApplicantCreatedIntegrationEventHandler: IIntegrationEventHandler<ApplicantCreatedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public ApplicantCreatedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApplicantCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _context.Applicants.AddAsync(new Domain.Entities.Applicant
        {
            Id = notification.Id
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}