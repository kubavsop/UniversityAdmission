using Admission.Dictionary.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateDocumentType;

namespace Admission.Dictionary.Application.IntegrationEvents;

public sealed class UpdateDocumentTypeIntegrationEventHandler: IIntegrationEventHandler<UpdateDocumentTypeRequest>
{
    private readonly IImporterService _importerService;

    public UpdateDocumentTypeIntegrationEventHandler(IImporterService importerService)
    {
        _importerService = importerService;
    }


    public async Task Handle(UpdateDocumentTypeRequest notification, CancellationToken cancellationToken)
    {
        if (notification.Role != RoleType.Admin) return;

        await _importerService.UpdateDocumentTypesAsync();
    }
}