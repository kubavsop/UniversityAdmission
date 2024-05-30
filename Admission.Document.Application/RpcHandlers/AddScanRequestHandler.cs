using System.Net.Mime;
using Admission.Application.Common.Constants;
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
        
        var stream = new MemoryStream(request.ScanModelResponse.Bytes);
        var file =  new FormFile(stream, 0, request.ScanModelResponse.Bytes.Length, request.ScanModelResponse.Name, request.ScanModelResponse.Name + ContentTypeMappings.ReverseTypeMappings[request.ScanModelResponse.ContentType])
        {
            Headers = new HeaderDictionary(),
            ContentType = request.ScanModelResponse.ContentType
        };

        var result = await _documentService.AddScan(documentType.ApplicantId, documentType.Id, new CreateScanDto { File = file }, true);
        if (result.IsFailure) return new RpcErrorResponse(result.Exception.Message);

        return new RpcOkResponse();
    }
}