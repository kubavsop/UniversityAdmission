using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Passport;

namespace Admission.Application.IntegrationEvents.Passport;

public sealed class PassportChangedIntegrationEventHandler: IIntegrationEventHandler<PassportChangedIntegrationEvent>
{
    private readonly IIntegrationAdmissionService _admissionService;

    public PassportChangedIntegrationEventHandler(IIntegrationAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }

    public Task Handle(PassportChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _admissionService.HandleApplicantChangedAsync(notification.UserId);
    }
}