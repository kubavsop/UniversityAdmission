﻿using Admission.Domain.Common.Enums;
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

        var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == managerId);
        var studentAdmission = await _context.StudentAdmissions
                .FirstOrDefaultAsync(sa => sa.ApplicantId == applicantId);

        if (studentAdmission == null || manager == null) return false;

        if (studentAdmission.ManagerId == managerId ||
            studentAdmission.FirstPriorityFacultyId == manager.FacultyId) return true;
        
        return false;
    }
}