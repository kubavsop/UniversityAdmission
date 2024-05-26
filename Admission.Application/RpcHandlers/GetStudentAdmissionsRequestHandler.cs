using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Entities;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class GetStudentAdmissionsRequestHandler: IRequestHandler<GetStudentAdmissionsRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;

    public GetStudentAdmissionsRequestHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(GetStudentAdmissionsRequest request, CancellationToken cancellationToken)
    {
        var normalizedApplicantName = request.ApplicantName?.ToUpper();
        var normalizedEducationProgramName = request.EducationProgramName?.ToUpper();
        
        var currentGroup = await _context.AdmissionGroups
            .OrderByDescending(g => g.CreateTime)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        var studentAdmissionsQueryable = _context.StudentAdmissions
            .GetUndeleted()
            .Where(sa => currentGroup != null && sa.AdmissionGroupId == currentGroup.Id)
            .Where(s => normalizedApplicantName == null ||
                        s.Applicant.FullName.ToUpper().Contains(normalizedApplicantName))
            .Where(s => normalizedEducationProgramName == null ||
                        s.EducationPrograms.Any(p => p.Name.ToUpper().Contains(normalizedEducationProgramName)))
            .Where(s => request.AdmissionStatus == null || s.Status == request.AdmissionStatus)
            .Where(s => request.WithoutManager == null || (request.WithoutManager.Value && s.ManagerId == null) ||
                        (!request.WithoutManager.Value && s.ManagerId.HasValue))
            .Where(s => !request.OnlyMine || (request.OnlyMine && s.ManagerId == request.Id))
            .Where(s => request.Faculties.Count == 0 ||
                        s.EducationPrograms.Any(p => request.Faculties.Contains(p.FacultyId)));

        var sortedQueryable = SortAdmissions(studentAdmissionsQueryable, request.SortingOptions);
        
        var result = await sortedQueryable
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .Select(sa => new StudentAdmissionResponse
            {
                Id = sa.Id,
                ExistManager = sa.ManagerId.HasValue,
                IsMyApplicant = sa.ManagerId == request.Id,
                ManagerName = sa.Manager == null ? null : sa.Manager.FullName,
                Status = sa.Status
            })
            .ToListAsync(cancellationToken: cancellationToken);
        
        if (result.Count == 0 && request.Page != 1)
        {
            return new RpcErrorResponse("Invalid value for attribute page");
        }
        
        var count = await studentAdmissionsQueryable.CountAsync(cancellationToken: cancellationToken);

        return new StudentAdmissionsResponse
        {
            Admissions = result,
            PageInfoModel = new PageInfoRpcModel()
            {
                Count = (int)Math.Ceiling((double)count / request.Size),
                Current = request.Page,
                Size = request.Size
            }
        };
    }
    
    
    private IQueryable<StudentAdmission> SortAdmissions(IQueryable<StudentAdmission> queryable, SortingOptions sorting)
    {
        var sortedQueryable = sorting switch
        {
            SortingOptions.LastModifiedDateDesc => queryable.OrderByDescending(p => p.CreateTime),
            SortingOptions.LastModifiedAsc => queryable.OrderBy(p => p.ModifiedTime),
            _ => throw new ArgumentOutOfRangeException(nameof(sorting), sorting, null)
        };

        return sortedQueryable;
    }
}