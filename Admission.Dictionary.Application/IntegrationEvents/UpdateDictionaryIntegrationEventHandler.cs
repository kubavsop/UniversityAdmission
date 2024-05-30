using Admission.Dictionary.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.UpdateDictionary;
using Admission.DTOs.RpcModels.Enums;

namespace Admission.Dictionary.Application.IntegrationEvents;

public sealed class UpdateDictionaryIntegrationEventHandler: IIntegrationEventHandler<UpdateDictionaryIntegrationEvent>
{
    private readonly IImporterService _importerService;

    public UpdateDictionaryIntegrationEventHandler(IImporterService importerService)
    {
        _importerService = importerService;
    }

    public async Task Handle(UpdateDictionaryIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Role != RoleType.Admin) return;
        
        switch (notification.Options)
        {
            case UpdateOptions.EducationLevel:
                await _importerService.UpdateEducationLevelsAsync();
                break;
            case UpdateOptions.DocumentType:
                await _importerService.UpdateDocumentTypesAsync();
                break;
            case UpdateOptions.EducationProgram:
                await _importerService.UpdateProgramsAsync();
                break;
            case UpdateOptions.Faculty:
                await _importerService.UpdateFacultiesAsync();
                break;
            case UpdateOptions.All:
                await _importerService.UpdateAllAsync();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}