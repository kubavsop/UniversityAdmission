using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService.ChangeEducationDocument;
using MediatR;

namespace Admission.Document.Application.RpcHandlers;

public sealed class ChangeEducationDocumentRequestHandler: IRequestHandler<ChangeEducationDocumentRequest, IRpcResponse>
{
    private readonly IDocumentService _documentService;
    private readonly IManagerAccessService _managerAccessService;
    private readonly IDocumentDbContext _context;

    public ChangeEducationDocumentRequestHandler(IDocumentService documentService, IManagerAccessService managerAccessService, IDocumentDbContext context)
    {
        _documentService = documentService;
        _managerAccessService = managerAccessService;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(ChangeEducationDocumentRequest request, CancellationToken cancellationToken)
    {
        var documentType = await _context.Documents.GetByIdAsync(request.DocumentId);
        if (documentType == null) return new RpcErrorResponse("Document not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, documentType.ApplicantId)) return new RpcErrorResponse("You have no rights");

        var result = await _documentService.EditEducationDocumentAsync(
            new EditEducationDocumentDto
                { EducationDocumentTypeId = request.EducationDocumentTypeId, Name = request.Name }, request.DocumentId,
            documentType.ApplicantId,true);
        if (result.IsFailure) return new RpcErrorResponse(result.Exception.Message);

        return new RpcOkResponse();
    }
}