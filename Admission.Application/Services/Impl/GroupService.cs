using Admission.Application.Common.Result;
using Admission.Application.Context;
using Admission.Application.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public sealed class GroupService: IGroupService
{
    private readonly IAdmissionDbContext _context;

    public GroupService(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<AdmissionGroupDto>>> GetGroupsAsync(Guid userId)
    {
        return await _context.AdmissionGroups
            .AsNoTracking()
            .Where(g => !g.DeleteTime.HasValue)
            .Select(g => new AdmissionGroupDto
        {
            Id = g.Id,
            CreateTime = g.CreateTime,
            Status = g.Status,
            ApplicantAdmission = g.Admissions.Where(a => a.ApplicantId == userId).Select(a => new StudentAdmissionShortDto
            {
                Id = a.Id,
                CreateTime = a.CreateTime,
                Status = a.Status,
                ExistManager = a.ManagerId != null,
                ModifiedTime = a.ModifiedTime
            }).FirstOrDefault()
        }).ToListAsync();
    }
}