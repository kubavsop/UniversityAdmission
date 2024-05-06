using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.EducationDocumentType;

public sealed class
    DocumentEducationLevelChangedIntegrationEventHandler : IIntegrationEventHandler<
    DocumentEducationLevelChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public DocumentEducationLevelChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DocumentEducationLevelChangedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        var type = await _context.EducationDocumentTypes.FirstOrDefaultAsync(t => t.Id == notification.Id,
            cancellationToken: cancellationToken);
        if (type == null) return;

        if (!await _context.EducationLevels.AnyAsync(l => l.ExternalId == notification.EducationLevelId,
                cancellationToken))
        {
            await _context.EducationLevels.AddAsync(new Domain.Entities.EducationLevel
            {
                ExternalId = notification.EducationLevelId,
                Name = notification.EducationLevelName
            }, cancellationToken);
        }

        type.EducationLevelId = notification.EducationLevelId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}