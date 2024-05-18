using Admission.DTOs.IntegrationEvents.Events.EducationDocument;

namespace Admission.Application.Services;

public interface IDocumentService
{
    Task CreateEducationDocumentAsync(EducationDocumentCreatedIntegrationEvent integrationEvent);
    Task ChangeDocumentNameAsync(EducationDocumentNameChangedIntegrationEvent integrationEvent);
    Task ChangeDocumentTypeAsync(EducationDocumentTypeChangedIntegrationEvent integrationEvent);
    Task ChangeDeleteTimeAsync(EducationDocumentDeleteTimeChangedIntegrationEvent integrationEvent);
}