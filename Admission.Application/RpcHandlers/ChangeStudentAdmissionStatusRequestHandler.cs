using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.ChangeStudentAdmissionStatus;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class ChangeStudentAdmissionStatusRequestHandler: IRequestHandler<ChangeStudentAdmissionStatusRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;
    private readonly IManagerAccessService _managerAccessService;

    public ChangeStudentAdmissionStatusRequestHandler(IAdmissionDbContext context, IManagerAccessService managerAccessService)
    {
        _context = context;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(ChangeStudentAdmissionStatusRequest request, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions
            .Include(sa => sa.Applicant)
            .GetByIdAsync(request.StudentAdmissionId);

        if (studentAdmission == null) return new RpcErrorResponse("Student admission not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, studentAdmission.ApplicantId))
            return new RpcErrorResponse("You have no rights");
        
        studentAdmission.ChangeStatus(request.Status);

        await _context.SaveChangesAsync(cancellationToken);

        return new RpcOkResponse();
    }
}