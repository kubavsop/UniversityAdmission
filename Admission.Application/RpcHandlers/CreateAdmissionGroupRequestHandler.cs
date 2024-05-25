using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.Domain.Entities;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.CreateAdmissionGroup;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class CreateAdmissionGroupRequestHandler: IRequestHandler<CreateAdmissionGroupRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;

    public CreateAdmissionGroupRequestHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(CreateAdmissionGroupRequest request, CancellationToken cancellationToken)
    {
        if (request.Role != RoleType.Admin) return new RpcErrorResponse("You have no rights");
        
        var admissions = await _context.StudentAdmissions
            .Include(a => a.Applicant)
            .Where(sa => !sa.DeleteTime.HasValue && sa.Status != AdmissionStatus.Closed)
            .ToListAsync(cancellationToken: cancellationToken);

        var groups =
            await _context.AdmissionGroups.Where(g =>
                !g.DeleteTime.HasValue && g.Status != AdmissionGroupStatus.Closed)
                .ToListAsync(cancellationToken: cancellationToken);
        
        foreach (var admission in admissions)
        {
            admission.ChangeStatus(AdmissionStatus.Closed);
        }

        foreach (var group in groups)
        {
            group.Status = AdmissionGroupStatus.Closed;
        }

        await _context.AdmissionGroups.AddAsync(new AdmissionGroup { Status = AdmissionGroupStatus.Open }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new RpcOkResponse();
    }
}