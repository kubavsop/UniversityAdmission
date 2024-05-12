namespace Admission.Application.Services;

public interface IAdmissionStatusService
{
    Task<bool> IsAdmissionStatusClosed(Guid userId);
    Task HandleStudentAdmissionChanged(Guid userId);
}