using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.Services;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.DownloadScan;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.RpcHandlers;

public sealed class DownloadResponseRequestHandler: IRequestHandler<DownloadScanRequest, IRpcResponse>
{
    private readonly IScanService _scanService;
    private readonly IDocumentDbContext _context;

    public DownloadResponseRequestHandler(IScanService scanService, IDocumentDbContext context)
    {
        _scanService = scanService;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(DownloadScanRequest request, CancellationToken cancellationToken)
    {
        if (request.Role == RoleType.Applicant) return new RpcErrorResponse("You have no rights");
        var file = await _context.Files
            .Include(f => f.Document)
            .GetByIdAsync(request.ScanId);
        
        if (file == null) return new RpcErrorResponse("File not found");
        
        var scanResult = await _scanService.GetScanAsync(file.Document.ApplicantId, request.ScanId);
        if (scanResult.IsFailure) return new RpcErrorResponse(scanResult.Exception.Message);

        return new ScanRpcFullModelResponse
        {
            ScanId = request.ScanId,
            Bytes = scanResult.Value.Bytes,
            ContentType = scanResult.Value.Extension,
            Name = scanResult.Value.Name
        };
    }
}