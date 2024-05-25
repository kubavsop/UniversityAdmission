using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Application.Context;
using Admission.Application.DTOs.Responses;
using Admission.Domain.Common.Enums;
using Admission.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public class AdmissionService: IAdmissionService
{
    private readonly IAdmissionDbContext _context;
    private readonly IMapper _mapper;
    public AdmissionService(IAdmissionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result> CreateAdmissionAsync(Guid userId)
    {
        var currentGroupId = (await _context.AdmissionGroups.FirstOrDefaultAsync(g => g.Status == AdmissionGroupStatus.Open))?.Id;
        
        if (currentGroupId == null)
        {
            return new BadRequestException("University admission is not currently available");
        }

        if (await _context.StudentAdmissions.AnyAsync(a => a.AdmissionGroupId == currentGroupId && a.ApplicantId == userId))
        {
            return new BadRequestException("Your university admission has already been created");
        }
        
        var applicant = await _context.Applicants.GetByIdAsync(userId);
        await _context.StudentAdmissions.AddAsync(StudentAdmission.Create(applicant, currentGroupId.Value));
        await _context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<StudentAdmissionDto>> GetAdmissionAsync(Guid admissionId, Guid userId)
    {
        var admission = await _context.StudentAdmissions
            .AsNoTracking()
            .Include(a => a.AdmissionPrograms.Where(ap => !ap.DeleteTime.HasValue))
                .ThenInclude(p => p.EducationProgram)
                    .ThenInclude(e => e.Faculty)
            .Include(a => a.AdmissionPrograms)
                .ThenInclude(p => p.EducationProgram)
                    .ThenInclude(e => e.EducationLevel)
            .GetByIdAsync(admissionId);
        
        if (admission == null)
        {
            return new NotFoundException(nameof(StudentAdmission), admissionId);
        }

        if (admission.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }

        return _mapper.Map<StudentAdmissionDto>(admission);
    }
}