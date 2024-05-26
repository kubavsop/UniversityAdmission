using System.Net.Mime;
using Admission.Application.Common.Constants;
using Admission.Application.Common.Exceptions;
using Admission.Document.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;
using MediatR;

namespace Admission.Document.Application.RpcHandlers;

public sealed class GetPassportRequestHandler: IRequestHandler<GetPassportRequest, IRpcResponse>
{
    private readonly IScanService _scanService;
    private readonly IDocumentService _documentService;
    private readonly IManagerAccessService _managerAccessService;
    
    public GetPassportRequestHandler(IScanService scanService, IDocumentService documentService, IManagerAccessService managerAccessService)
    {
        _scanService = scanService;
        _documentService = documentService;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(GetPassportRequest request, CancellationToken cancellationToken)
    {
        var passportResult = await _documentService.GetPassportAsync(request.ApplicantId);
        if (passportResult.IsFailure) return new RpcErrorResponse(passportResult.Exception.Message);

        var passport = passportResult.Value;
        var isEditable = await _managerAccessService.HasEditPermissions(request.Id, request.Role, request.ApplicantId);
        
        var scans = new LinkedList<ScanRpcModel>();
        foreach (var file in passport.Files)
        {
            var fileDto = await _scanService.GetScanAsync(request.ApplicantId, file.Id);
            scans.AddLast(new ScanRpcModel
            {
                ScanId = file.Id,
                Name = fileDto.Value.Name + ContentTypeMappings.ReverseTypeMappings[fileDto.Value.Extension],
                IsEditable = isEditable
            });
        }
        
        return new PassportResponse
        {
            DocumentId = passport.Id,
            Series = passport.Series,
            Number = passport.Number,
            PlaceOfBirth = passport.PlaceOfBirth,
            IssuedBy = passport.IssuedBy,
            DateIssued = passport.DateIssued,
            IsEditable = isEditable,
            Scans = scans
        };
    }
}