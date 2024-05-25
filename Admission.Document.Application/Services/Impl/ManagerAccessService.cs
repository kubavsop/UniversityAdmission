using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Domain.Entities;
using Admission.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.Services.Impl;

public class ManagerAccessService: IManagerAccessService
{
    private readonly IDocumentDbContext _context;

    public ManagerAccessService(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasEditPermissions(Guid managerId, RoleType managerRole, Guid applicantId)
    {
        if (managerRole == RoleType.Applicant) return false;
        if (managerRole >= RoleType.SeniorManager) return true;

        var manager = await _context.Managers.GetByIdAsync(managerId);
        var studentAdmission = await _context.StudentAdmissions
            .Where(sa => sa.ApplicantId == applicantId)
            .OrderByDescending(sa => sa.CreateTime)
            .FirstOrDefaultAsync();

        if (studentAdmission == null || manager == null) return false;

        if (studentAdmission.ManagerId == managerId ||
            studentAdmission.FirstPriorityFacultyId == manager.FacultyId) return true;
        
        return false;
    }
    
    public bool HasEditPermissions(Manager? manager, RoleType managerRole, StudentAdmission? studentAdmission)
    {
        if (managerRole == RoleType.Applicant) return false;
        if (managerRole >= RoleType.SeniorManager) return true;
        
        if (studentAdmission == null || manager == null) return false;
        
        if (studentAdmission.ManagerId == manager.Id ||
            studentAdmission.FirstPriorityFacultyId == manager.FacultyId) return true;
        
        return false;
    }
}