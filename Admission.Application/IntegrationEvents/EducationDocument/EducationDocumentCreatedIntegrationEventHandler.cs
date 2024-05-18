using Admission.Application.Services;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Application.IntegrationEvents.EducationDocument;

public sealed class EducationDocumentCreatedIntegrationEventHandler: IIntegrationEventHandler<EducationDocumentCreatedIntegrationEvent>
{
    private readonly IDocumentService _documentService;

    public EducationDocumentCreatedIntegrationEventHandler(IIntegrationAdmissionService admissionService, IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task Handle(EducationDocumentCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _documentService.CreateEducationDocumentAsync(notification);
    }
}