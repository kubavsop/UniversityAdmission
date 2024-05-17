using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public sealed class IntegrationAdmissionService : IIntegrationAdmissionService
{
    private readonly IAdmissionDbContext _context;

    public IntegrationAdmissionService(IAdmissionDbContext context)
    {
        _context = context;
    }
    
    private async Task<bool> IsAdmissionStatusClosed(Guid userId)
    {
        var admissions = await _context.StudentAdmissions
            .GetUndeleted()
            .Where(sa => sa.ApplicantId == userId)
            .ToListAsync();

        return admissions.Count != 0 && !admissions.Any(
            sa => sa.ApplicantId == userId && sa.Status != AdmissionStatus.Closed);
    }

    private async Task HandleApplicantAdmissionChangedAsync(Guid userId)
    {
        var currentAdmission = await _context.StudentAdmissions
            .GetUndeleted()
            .Include(a => a.Applicant)
            .FirstOrDefaultAsync(sa => sa.ApplicantId == userId &&
                                       sa.Status != AdmissionStatus.Closed);
        
        if (currentAdmission == null || currentAdmission.Status == AdmissionStatus.Closed) return;
                    
        currentAdmission.ChangeStatus(currentAdmission.ManagerId != null
            ? AdmissionStatus.UnderReview
            : AdmissionStatus.Created);
    }

    public async Task ChangeApplicantEmailAsync(Guid userId, string email)
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == userId);
        
        if (applicant == null) return;

        applicant.Email = email;
        
        await HandleApplicantAdmissionChangedAsync(userId);    
        
        await _context.SaveChangesAsync();
    }

    public async Task ChangeApplicantFullNameAsync(Guid userId, string fullName)
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == userId);
        
        if (applicant == null) return;
        
        applicant.FullName = fullName;

        await HandleApplicantAdmissionChangedAsync(userId);    

        await _context.SaveChangesAsync();   
    }

    public async Task HandleApplicantChangedAsync(Guid userId)
    {
        await HandleApplicantAdmissionChangedAsync(userId);

        await _context.SaveChangesAsync();
    }
}