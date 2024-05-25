using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public sealed class ManagerAccessService: IManagerAccessService
{
    private readonly IAdmissionDbContext _context;

    public ManagerAccessService(IAdmissionDbContext context)
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

        var program = await _context.AdmissionPrograms
            .Include(p => p.EducationProgram)
            .Where(p => p.StudentAdmissionId == studentAdmission.Id)
            .OrderBy(p => p.Priority)
            .FirstOrDefaultAsync();

        
        return studentAdmission.ManagerId == managerId ||
               program?.EducationProgram.FacultyId == manager.FacultyId;
    }
}