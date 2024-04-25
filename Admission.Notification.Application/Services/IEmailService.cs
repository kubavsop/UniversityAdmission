
using Admission.DTOs.IntegrationEvents.Events.Email;

namespace Admission.Notification.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(MailRequestIntegrationEvent requestIntegrationEvent);
}