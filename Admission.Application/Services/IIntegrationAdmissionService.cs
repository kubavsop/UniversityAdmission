namespace Admission.Application.Services;

public interface IIntegrationAdmissionService
{
    Task ChangeApplicantEmailAsync(Guid userId, string email);
    Task ChangeApplicantFullNameAsync(Guid userId, string fullName);
    Task HandleApplicantChangedAsync(Guid userId);
}