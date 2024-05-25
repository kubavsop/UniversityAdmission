using Admission.Application.Common.Extensions;
using Admission.Document.Application.Context;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.RpcHandlers;

public sealed class GetEducationDocumentsRequestHandler: IRequestHandler<GetEducationDocumentsRequest, IRpcResponse>
{
    private readonly IDocumentService _documentService;
    private readonly IDocumentDbContext _context;
    private readonly IManagerAccessService _managerAccessService;
    private readonly IScanService _scanService;

    public GetEducationDocumentsRequestHandler(IDocumentService documentService, IManagerAccessService managerAccessService, IDocumentDbContext context, IScanService scanService)
    {
        _documentService = documentService;
        _managerAccessService = managerAccessService;
        _context = context;
        _scanService = scanService;
    }

    public async Task<IRpcResponse> Handle(GetEducationDocumentsRequest request, CancellationToken cancellationToken)
    {
        var documents = await _documentService.GetEducationDocumentAsync(request.ApplicantId);
        if (documents.IsFailure) return new RpcErrorResponse(documents.Exception.Message);
        
        var manager = await _context.Managers.GetByIdAsync(request.Id);
        var studentAdmission = await _context.StudentAdmissions
            .Where(sa => sa.ApplicantId == request.ApplicantId)
            .OrderByDescending(sa => sa.CreateTime)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var documentResponses = new LinkedList<EducationDocumentResponse>();

        foreach (var document in documents.Value)
        {
            var scans = new LinkedList<ScanRpcModel>();
            foreach (var file in document.Files)
            {
                var fileDto = await _scanService.GetScanAsync(request.ApplicantId, file.Id);
                scans.AddLast(new ScanRpcModel
                {
                    Id = file.Id,
                    Bytes = fileDto.Value.Bytes,
                    ContentType = fileDto.Value.Extension,
                    Name = fileDto.Value.Name
                });
            }

            documentResponses.AddLast(new EducationDocumentResponse
            {
                Scans = scans,
                EducationDocumentId = document.Id,
                Name = document.Name,
                EducationDocumentTypeId = document.EducationDocumentType.Id,
            });
        }
        
        return new EducationDocumentsResponse
        {
            DocumentResponses = documentResponses,
            IsEditable = _managerAccessService.HasEditPermissions(manager, request.Role, studentAdmission)
        };
    }
}