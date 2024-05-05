using Admission.Application.Common.Extensions;
using Admission.DTOs.IntegrationEvents;
using Admission.DTOs.IntegrationEvents.Events.StudentAdmission;
using Admission.User.Application.Context;

namespace Admission.User.Application.IntegrationEvents.Admission;

public sealed class
    AdmissionManagerChangedIntegrationEventHandler : IIntegrationEventHandler<AdmissionManagerChangedIntegrationEvent>
{
    private readonly IUserDbContext _context;

    public AdmissionManagerChangedIntegrationEventHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AdmissionManagerChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.GetByIdAsync(notification.Id);
        if (studentAdmission == null) return;

        studentAdmission.ManagerId = notification.ManagerId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}