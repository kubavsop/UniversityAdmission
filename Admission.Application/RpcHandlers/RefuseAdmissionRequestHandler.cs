using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.RefuseAdmission;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class RefuseAdmissionRequestHandler: IRequestHandler<RefuseAdmissionRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;

    public RefuseAdmissionRequestHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(RefuseAdmissionRequest request, CancellationToken cancellationToken)
    {
        if (request.ManagerId != request.Id && request.Role < RoleType.SeniorManager)
            return new RpcErrorResponse("You have no rights");
        
        var studentAdmission = await _context.StudentAdmissions
            .Include(sa => sa.Applicant)
            .GetByIdAsync(request.StudentAdmissionId);
        
        if (studentAdmission == null) return new RpcErrorResponse("StudentAdmission not found");
        
        if (studentAdmission.ManagerId != request.ManagerId) return new RpcErrorResponse("Invalid request");

        if (studentAdmission.Status == AdmissionStatus.UnderReview)
        {
            studentAdmission.ChangeStatus(AdmissionStatus.Created);
        }
        studentAdmission.ChangeManager(null);
        
        await _context.SaveChangesAsync(cancellationToken);
        return new RpcOkResponse();
    }
}