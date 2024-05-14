using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;

namespace Admission.Application.IntegrationEvents.Applicant;

public sealed class ApplicantChangedIntegrationEventHandler: IIntegrationEventHandler<ApplicantChangedIntegrationEvent>
{
    private readonly IIntegrationAdmissionService _admissionService;

    public ApplicantChangedIntegrationEventHandler(IIntegrationAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }

    public Task Handle(ApplicantChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _admissionService.HandleApplicantChangedAsync(notification.Id);
    }
}