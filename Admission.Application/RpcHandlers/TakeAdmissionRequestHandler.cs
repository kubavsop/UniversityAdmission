using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.TakeAdmission;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class TakeAdmissionRequestHandler: IRequestHandler<TakeAdmissionRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;

    public TakeAdmissionRequestHandler(IAdmissionDbContext context)
    {
        _context = context;
    }


    public async Task<IRpcResponse> Handle(TakeAdmissionRequest request, CancellationToken cancellationToken)
    {
        if (request.ManagerId != request.Id && request.Role < RoleType.SeniorManager)
            return new RpcErrorResponse("You have no rights");
        
        var studentAdmission = await _context.StudentAdmissions
            .Include(sa => sa.Applicant)
            .GetByIdAsync(request.StudentAdmissionId);
        
        if (studentAdmission == null) return new RpcErrorResponse("StudentAdmission not found");
        if (studentAdmission.ManagerId != null) return new RpcErrorResponse("Invalid request");

        var manager = await _context.Managers.GetByIdAsync(request.ManagerId);
        if (manager == null) return new RpcErrorResponse("Manager not found");
        
        studentAdmission.ChangeManager(manager);

        await _context.SaveChangesAsync(cancellationToken);

        return new RpcOkResponse();
    }
}