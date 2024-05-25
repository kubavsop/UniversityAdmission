using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService.DeleteScan;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.RpcHandlers;

public sealed class DeleteScanRequestHandler: IRequestHandler<DeleteScanRequest, IRpcResponse>
{
    private readonly IManagerAccessService _managerAccessService;
    private readonly IDocumentDbContext _context;
    private readonly IScanService _scanService;

    public DeleteScanRequestHandler(IManagerAccessService managerAccessService, IDocumentDbContext context, IScanService scanService)
    {
        _managerAccessService = managerAccessService;
        _context = context;
        _scanService = scanService;
    }

    public async Task<IRpcResponse> Handle(DeleteScanRequest request, CancellationToken cancellationToken)
    {
        var file = await _context.Files
            .Include(f => f.Document)
            .GetByIdAsync(request.ScanId);
        
        if (file == null) return new RpcErrorResponse("Scan not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, file.Document.ApplicantId)) return new RpcErrorResponse("You have no rights");

        var result = await _scanService.DeleteScanAsync(file.Document.ApplicantId, request.ScanId);
        if (result.IsFailure) return new RpcErrorResponse(result.Exception.Message);

        return new RpcOkResponse();
    }
}