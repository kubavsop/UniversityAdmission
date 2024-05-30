using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmission;
using Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class GetStudentAdmissionRequestHandler: IRequestHandler<GetStudentAdmissionRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;
    private readonly IManagerAccessService _managerAccessService;
    
    public GetStudentAdmissionRequestHandler(IAdmissionDbContext context, IManagerAccessService managerAccessService)
    {
        _context = context;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(GetStudentAdmissionRequest request, CancellationToken cancellationToken)
    {
        var admission = await _context.StudentAdmissions
            .Include(a => a.Applicant)
            .Include(a => a.Manager)
            .GetByIdAsync(request.StudentAdmissionId);
        if (admission == null) return new RpcErrorResponse("Student admission not found");

        return new StudentAdmissionResponse
        {
            AdmissionId = admission.Id,
            ExistManager = admission.ManagerId != null,
            IsMyApplicant = admission.ManagerId == request.Id,
            ManagerName = admission.Manager?.FullName,
            Status = admission.Status,
            ApplicantId = admission.ApplicantId,
            ApplicantEmail = admission.Applicant.Email,
            IsEditable = await _managerAccessService.HasEditPermissions(request.Id, request.Role, admission.ApplicantId),
            ManagerId = admission.ManagerId
        };
    }
}