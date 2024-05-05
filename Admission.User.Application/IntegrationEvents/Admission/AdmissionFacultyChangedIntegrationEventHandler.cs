using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Admission.User.Application.Context;

namespace Admission.User.Application.IntegrationEvents.Admission;

public sealed class AdmissionFacultyChangedIntegrationEventHandler: IIntegrationEventHandler<AdmissionFacultyChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public AdmissionFacultyChangedIntegrationEventHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionFacultyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.GetByIdAsync(notification.Id);
        if (studentAdmission == null) return;
        
        studentAdmission.FirstPriorityFacultyId = notification.FirstPriorityFacultyId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}