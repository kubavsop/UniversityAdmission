using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.EducationDocumentType;

public sealed class
    DocumentDeleteTimeChangedIntegrationEventHandler : IIntegrationEventHandler<
    DocumentDeleteTimeChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public DocumentDeleteTimeChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DocumentDeleteTimeChangedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        var type = await _context.EducationDocumentTypes.FirstOrDefaultAsync(t => t.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (type == null) return;

        type.DeleteTime = notification.DeleteTime;

        await _context.SaveChangesAsync(cancellationToken);
    }
}