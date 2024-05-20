﻿using Admission.Document.Application.Context;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.Manager;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.IntegrationEvents.Manager;

public sealed class ManagerFacultyChangedIntegrationEventHandler: IIntegrationEventHandler<ManagerFacultyChangedIntegrationEvent>
{
    private readonly IDocumentDbContext _context;

    public ManagerFacultyChangedIntegrationEventHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ManagerFacultyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == notification.Id, cancellationToken: cancellationToken);

        if (manager == null) return;
        
        if (!await _context.Faculties.AnyAsync(f => f.Id == notification.FacultyId, cancellationToken: cancellationToken))
        {
            await _context.Faculties.AddAsync(new Domain.Entities.Faculty
            {
                Id = notification.FacultyId,
            }, cancellationToken);
        }

        manager.FacultyId = notification.FacultyId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}