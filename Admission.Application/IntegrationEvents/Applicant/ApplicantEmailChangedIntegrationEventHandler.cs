using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Applicant;

public sealed class ApplicantEmailChangedIntegrationEventHandler: IIntegrationEventHandler<ApplicantEmailChangedIntegrationEvent>
{
    private readonly IIntegrationAdmissionService _admissionService;

    public ApplicantEmailChangedIntegrationEventHandler(IIntegrationAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }


    public Task Handle(ApplicantEmailChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _admissionService.ChangeApplicantEmailAsync(notification.Id, notification.Email);
    }
}