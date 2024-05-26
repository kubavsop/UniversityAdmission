using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Application.DTOs.Requests;
using Admission.Application.Services;
using Admission.Application.Services.Impl;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.ChangeProgramPriorities;
using Admission.DTOs.RpcModels.Base;
using MediatR;

namespace Admission.Application.RpcHandlers;

public sealed class ChangeProgramPrioritiesRequestHandler: IRequestHandler<ChangeProgramsPrioritiesRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;
    private readonly IProgramService _programService;
    private readonly IManagerAccessService _managerAccessService;

    public ChangeProgramPrioritiesRequestHandler(IProgramService programService, IManagerAccessService managerAccessService, IAdmissionDbContext context)
    {
        _programService = programService;
        _managerAccessService = managerAccessService;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(ChangeProgramsPrioritiesRequest request, CancellationToken cancellationToken)
    {
        var studentAdmission = await _context.StudentAdmissions.GetByIdAsync(request.StudentAdmissionId);

        if (studentAdmission == null) return new RpcErrorResponse("Student admission not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, studentAdmission.ApplicantId))
            return new RpcErrorResponse("You have no rights");

        var programResult = await _programService.EditProgramsAsync(new EditProgramsDto
        {
            EditPrograms = request.Programs.Select(p => new EditProgramDto
            {
                AdmissionProgramId = p.Id,
                Priority = p.Priority
            })
        }, studentAdmission.ApplicantId, true);

        if (programResult.IsFailure) return new RpcErrorResponse(programResult.Exception.Message);

        return new RpcOkResponse();
    }
}