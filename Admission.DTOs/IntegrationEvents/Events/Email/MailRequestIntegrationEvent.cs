
namespace Admission.DTOs.IntegrationEvents.Events.Email;

public class MailRequestIntegrationEvent: IIntegrationEvent
{
    public required string EmailTo { get; init; }
    public required string Subject { get; init;  }
    public required string Body { get; init; }
}