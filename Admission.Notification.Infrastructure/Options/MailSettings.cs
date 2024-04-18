namespace Admission.Notification.Infrastructure.Options;

public sealed class MailSettings
{
    public required string SenderName { get; init; }
    public required string SenderEmail { get; init; }
    public required string SmtpServer { get; init; }
    public int SmtpPort { get; init; }
}