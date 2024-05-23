using System.Collections.Immutable;
using Admission.Dictionary.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateProgram;

namespace Admission.Dictionary.Application.IntegrationEvents;

public sealed class UpdateProgramIntegrationEventHandler: IIntegrationEventHandler<UpdateProgramRequest>
{
    private readonly IImporterService _importerService;

    public UpdateProgramIntegrationEventHandler(IImporterService importerService)
    {
        _importerService = importerService;
    }

    public async Task Handle(UpdateProgramRequest notification, CancellationToken cancellationToken)
    {
        if (notification.Role != RoleType.Admin) return;

        await _importerService.UpdateProgramsAsync();
    }
}