using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.FIle;

namespace Admission.Application.IntegrationEvents.File;

public sealed class FileChangedIntegrationEventHandler: IIntegrationEventHandler<FileChangedIntegrationEvent>
{
    private readonly IIntegrationAdmissionService _admissionService;

    public FileChangedIntegrationEventHandler(IIntegrationAdmissionService admissionService)
    {
        _admissionService = admissionService;
    }
    
    public Task Handle(FileChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _admissionService.HandleApplicantChangedAsync(notification.UserId);
    }
}