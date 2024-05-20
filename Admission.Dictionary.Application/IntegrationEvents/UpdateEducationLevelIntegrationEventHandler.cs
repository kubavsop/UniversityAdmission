using Admission.Dictionary.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateEducationLevel;

namespace Admission.Dictionary.Application.IntegrationEvents;

public sealed class UpdateEducationLevelIntegrationEventHandler: IIntegrationEventHandler<UpdateEducationLevelRequest>
{
    private readonly IImporterService _importerService;

    public UpdateEducationLevelIntegrationEventHandler(IImporterService importerService)
    {
        _importerService = importerService;
    }
    
    public async Task Handle(UpdateEducationLevelRequest notification, CancellationToken cancellationToken)
    {
        if (notification.Role != RoleType.Admin) return;

        await _importerService.UpdateEducationLevelsAsync();
    }
}