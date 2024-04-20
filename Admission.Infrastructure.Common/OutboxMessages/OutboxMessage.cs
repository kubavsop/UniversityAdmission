namespace Admission.Infrastructure.Common.OutboxMessages;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Content { get; set; }
    public DateTime OccurredTime { get; set; }
    public DateTime? ProcessedTime { get; set; }
}