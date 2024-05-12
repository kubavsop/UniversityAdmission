using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;

namespace Admission.Application.IntegrationEvents.Applicant;

public sealed class ApplicantChangedIntegrationEventHandler: IIntegrationEventHandler<ApplicantChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;
    
    public Task Handle(ApplicantChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}