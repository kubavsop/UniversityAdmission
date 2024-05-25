using System.Net.Mime;
using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService.AddScan;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Admission.Document.Application.RpcHandlers;

public sealed class AddScanRequestHandler: IRequestHandler<AddScanRequest, IRpcResponse>
{
    private readonly IManagerAccessService _managerAccessService;
    private readonly IDocumentDbContext _context;
    private readonly IDocumentService _documentService;
    
    public AddScanRequestHandler(IManagerAccessService managerAccessService, IDocumentService documentService, IDocumentDbContext context)
    {
        _managerAccessService = managerAccessService;
        _documentService = documentService;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(AddScanRequest request, CancellationToken cancellationToken)
    {
        var documentType = await _context.Documents.GetByIdAsync(request.DocumentId);
        if (documentType == null) return new RpcErrorResponse("Document not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, documentType.ApplicantId)) return new RpcErrorResponse("You have no rights");
        
        var stream = new MemoryStream(request.ScanModel.Bytes);
        var file =  new FormFile(stream, 0, request.ScanModel.Bytes.Length, request.ScanModel.Name, request.ScanModel.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = request.ScanModel.ContentType
        };

        var result = await _documentService.AddScan(documentType.ApplicantId, documentType.Id, new CreateScanDto { File = file });
        if (result.IsFailure) return new RpcErrorResponse(result.Exception.Message);

        return new RpcOkResponse();
    }
}