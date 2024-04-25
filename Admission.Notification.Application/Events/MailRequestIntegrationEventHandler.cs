using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Email;
using Admission.Notification.Application.Services;

namespace Admission.Notification.Application.Events;

public sealed class MailRequestIntegrationEventHandler: IIntegrationEventHandler<MailRequestIntegrationEvent>
{
    private readonly IEmailService _emailService;

    public MailRequestIntegrationEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(MailRequestIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(notification);
    }
}