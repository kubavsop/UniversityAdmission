using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Applicant;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.Applicant;

public sealed class ApplicantFullNameChangedIntegrationEventHandler: IIntegrationEventHandler<ApplicantFullNameChangedIntegrationEvent>
{
    private readonly IIntegrationAdmissionService _admissionService;

    public ApplicantFullNameChangedIntegrationEventHandler(IIntegrationAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }


    public Task Handle(ApplicantFullNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _admissionService.ChangeApplicantFullNameAsync(notification.Id, notification.FullName);
    }
}