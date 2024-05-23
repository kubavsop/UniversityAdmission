using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Application.IntegrationEvents.EducationDocument;

public sealed class EducationDocumentNameChangedIntegrationEventHandler: IIntegrationEventHandler<EducationDocumentNameChangedIntegrationEvent>
{
    private readonly IDocumentService _documentService;

    public EducationDocumentNameChangedIntegrationEventHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task Handle(EducationDocumentNameChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _documentService.ChangeDocumentNameAsync(notification);
    }
}