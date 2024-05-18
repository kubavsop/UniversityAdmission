using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Application.IntegrationEvents.EducationDocument;

public sealed class EducationDocumentDeleteTimeChangedIntegrationEventHandler: IIntegrationEventHandler<EducationDocumentDeleteTimeChangedIntegrationEvent>
{
    private readonly IDocumentService _documentService;

    public EducationDocumentDeleteTimeChangedIntegrationEventHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task Handle(EducationDocumentDeleteTimeChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _documentService.ChangeDeleteTimeAsync(notification);
    }
}