using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Application.IntegrationEvents.EducationDocument;

public sealed class EducationDocumentTypeChangedIntegrationEventHandler: IIntegrationEventHandler<EducationDocumentTypeChangedIntegrationEvent>
{
    private readonly IDocumentService _documentService;

    public EducationDocumentTypeChangedIntegrationEventHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task Handle(EducationDocumentTypeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _documentService.ChangeDocumentTypeAsync(notification);
    }
}