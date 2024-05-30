using Admission.Application.Common.Extensions;
using Admission.Domain.Common.Enums;
using Admission.User.Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Services.Impl;

public sealed class ManagerAccessService: IManagerAccessService
{
    private readonly IUserDbContext _context;

    public ManagerAccessService(IUserDbContext context)
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

        return studentAdmission.ManagerId == managerId ||
               studentAdmission.FirstPriorityFacultyId == manager.FacultyId;
    }
}