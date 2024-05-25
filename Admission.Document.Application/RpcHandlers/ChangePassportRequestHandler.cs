using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService.ChangePassport;
using MediatR;

namespace Admission.Document.Application.RpcHandlers;

public sealed class ChangePassportRequestHandler: IRequestHandler<ChangePassportRequest, IRpcResponse>
{
    private readonly IDocumentService _documentService;
    private readonly IManagerAccessService _managerAccessService;
    private readonly IDocumentDbContext _context;

    public ChangePassportRequestHandler(IDocumentDbContext context, IDocumentService documentService, IManagerAccessService managerAccessService)
    {
        _context = context;
        _documentService = documentService;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(ChangePassportRequest request, CancellationToken cancellationToken)
    {
        var documentType = await _context.Documents.GetByIdAsync(request.PassportId);
        if (documentType == null) return new RpcErrorResponse("Document not found");

        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, documentType.ApplicantId)) return new RpcErrorResponse("You have no rights");

        var result = await _documentService.EditPassportAsync(new EditPassportDto
        {
            Series = request.Series,
            Number = request.Number,
            PlaceOfBirth = request.PlaceOfBirth,
            IssuedBy = request.IssuedBy,
            DateIssued = request.DateIssued
        }, documentType.ApplicantId, true);
        if (result.IsFailure) return new RpcErrorResponse(result.Exception.Message);

        return new RpcOkResponse();
    }
}