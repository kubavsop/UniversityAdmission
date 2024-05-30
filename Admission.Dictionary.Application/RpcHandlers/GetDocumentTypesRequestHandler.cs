using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DictionaryService.GetDocumentTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetDocumentTypesRequestHandler: IRequestHandler<GetDocumentTypesRequest, IRpcResponse>
{
    private readonly IDictionaryDbContext _context;

    public GetDocumentTypesRequestHandler(IDictionaryDbContext context)
    {
        _context = context;
    }


    public async Task<IRpcResponse> Handle(GetDocumentTypesRequest request, CancellationToken cancellationToken)
    {
        if (request.Role == RoleType.Applicant) return new RpcErrorResponse("You have no rights");
        var result = await _context.DocumentTypes
            .AsNoTracking()
            .GetUndeleted()
            .Select(d => new DocumentTypeShortResponse
            {
                DocumentTypeId = d.Id,
                DocumentTypeName = d.Name
            }).ToListAsync(cancellationToken: cancellationToken);

        return new DocumentTypesResponse
        {
            DocumentTypes = result
        };
    }
}