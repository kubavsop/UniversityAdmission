﻿using Admission.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.DocumentType;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.IntegrationEvents.DocumentType;

public sealed class DocumentDeleteTimeChangedIntegrationEventHandler : IIntegrationEventHandler<
    DocumentDeleteTimeChangedIntegrationEvent>
{
    private readonly IAdmissionDbContext _context;

    public DocumentDeleteTimeChangedIntegrationEventHandler(IAdmissionDbContext context)
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