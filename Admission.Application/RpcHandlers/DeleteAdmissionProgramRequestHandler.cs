using Admission.Application.Common.Extensions;
using Admission.Application.Context;
using Admission.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.DeleteAdmissionProgram;
using Admission.DTOs.RpcModels.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class DeleteAdmissionProgramRequestHandler: IRequestHandler<DeleteAdmissionProgramRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;
    private readonly IManagerAccessService _managerAccessService;
    private readonly IProgramService _programService;
    
    public DeleteAdmissionProgramRequestHandler(IManagerAccessService managerAccessService, IAdmissionDbContext context, IProgramService programService)
    {

        _managerAccessService = managerAccessService;
        _context = context;
        _programService = programService;
    }

    public async Task<IRpcResponse> Handle(DeleteAdmissionProgramRequest request, CancellationToken cancellationToken)
    {
        var program = await _context.AdmissionPrograms
            .Include(p => p.StudentAdmission)
            .GetByIdAsync(request.AdmissionProgramId);
        
        if (program == null) return new RpcErrorResponse("Program not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, program.StudentAdmission.ApplicantId))
            return new RpcErrorResponse("You have no rights");

        var programResult = await _programService.DeleteProgramAsync(program.Id, program.StudentAdmission.ApplicantId, true);
        if (programResult.IsFailure) return new RpcErrorResponse(programResult.Exception.Message);

        return new RpcOkResponse();
    }
}