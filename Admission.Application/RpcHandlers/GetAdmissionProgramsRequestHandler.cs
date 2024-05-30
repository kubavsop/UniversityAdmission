using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class GetAdmissionProgramsRequestHandler: IRequestHandler<GetAdmissionProgramsRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;
    private readonly IManagerAccessService _managerAccessService;

    public GetAdmissionProgramsRequestHandler(IAdmissionDbContext context, IManagerAccessService managerAccessService)
    {
        _context = context;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(GetAdmissionProgramsRequest request, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.GetByIdAsync(request.StudentAdmissionId);

        if (studentAdmission == null) return new RpcErrorResponse("Student admission not found");

        var programs = await _context.AdmissionPrograms
            .AsNoTracking()
            .Where(ap => !ap.DeleteTime.HasValue && ap.StudentAdmissionId == request.StudentAdmissionId)
            .Select(p => new AdmissionProgramResponse
            {
                Id = p.Id,
                Priority = p.Priority,
                EducationProgramName = p.EducationProgram.Name
            }).ToListAsync(cancellationToken: cancellationToken);

        return new AdmissionProgramsResponse
        {
            Programs = programs,
            IsEditable = await _managerAccessService.HasEditPermissions(request.Id, request.Role, studentAdmission.ApplicantId)
        };
    }
}