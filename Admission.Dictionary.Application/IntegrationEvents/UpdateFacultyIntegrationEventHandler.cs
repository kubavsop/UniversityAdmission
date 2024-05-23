using Admission.Dictionary.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateFaculty;

namespace Admission.Dictionary.Application.IntegrationEvents;

public sealed class UpdateFacultyIntegrationEventHandler: IIntegrationEventHandler<UpdateFacultyRequest>
{
    private readonly IImporterService _importerService;

    public UpdateFacultyIntegrationEventHandler(IImporterService importerService)
    {
        _importerService = importerService;
    }

    public async Task Handle(UpdateFacultyRequest notification, CancellationToken cancellationToken)
    {
        if (notification.Role != RoleType.Admin) return;

        await _importerService.UpdateFacultiesAsync();
    }
}