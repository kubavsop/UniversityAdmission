using System.Net.Mail;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.Notification.Application.Services;
using Admission.Notification.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Admission.Notification.Infrastructure.Services;

public sealed class EmailService: IEmailService
{
    private readonly MailSettings _mailSettings;
    
    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    
    public async Task SendEmailAsync(MailRequestIntegrationEvent requestIntegrationEvent)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
            Subject = requestIntegrationEvent.Subject,
            Body = requestIntegrationEvent.Body
        };
        
        message.To.Add(requestIntegrationEvent.EmailTo);

        using (var client = new SmtpClient(_mailSettings.SmtpServer, _mailSettings.SmtpPort))
        {
            await client.SendMailAsync(message);
        }
    }
}