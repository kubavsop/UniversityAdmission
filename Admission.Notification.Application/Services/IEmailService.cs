using Admission.Application.Common.Messaging.IntegrationEvent.Events.Email;

namespace Admission.Notification.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(MailRequestIntegrationEvent requestIntegrationEvent);
}