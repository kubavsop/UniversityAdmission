using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public sealed class AdmissionStatusService : IAdmissionStatusService
{
    private readonly IAdmissionDbContext _context;

    public AdmissionStatusService(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsAdmissionStatusClosed(Guid userId)
    {
        var admissions = await _context.StudentAdmissions
            .GetUndeleted()
            .Where(sa => sa.ApplicantId == userId)
            .ToListAsync();

        return admissions.Count != 0 && !admissions.Any(
            sa => sa.ApplicantId == userId && sa.Status != AdmissionStatus.Closed);
    }

    public async Task HandleStudentAdmissionChanged(Guid userId)
    {
        var currentAdmission = await _context.StudentAdmissions
            .GetUndeleted()
            .FirstOrDefaultAsync(sa => sa.ApplicantId == userId &&
                                       sa.Status != AdmissionStatus.Closed);

        if (currentAdmission == null) return;
        
        if (currentAdmission.ManagerId != null) // admission changing, when manager

        throw new NotImplementedException();
    }
}