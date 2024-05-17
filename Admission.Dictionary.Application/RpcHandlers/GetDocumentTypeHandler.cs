using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.DocumentType;
using Admission.DTOs.RpcModels.EducationLevel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetDocumentTypeHandler: IRequestHandler<GetDocumentTypeRequest, IRpcResponse?>
{
    private readonly IDictionaryDbContext _context;

    public GetDocumentTypeHandler(IDictionaryDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse?> Handle(GetDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _context.DocumentTypes
            .GetUndeleted()
            .Where(t => t.Id == request.Id)
            .Where(d => !d.EducationLevel.DeleteTime.HasValue)
            .Select(t => new DocumentTypeResponse
            {
                Id = t.Id,
                Name = t.Name,
                EducationLevel = new EducationLevelResponse
                {
                    Id = t.EducationLevel.Id,
                    ExternalId = t.EducationLevel.ExternalId,
                    Name = t.EducationLevel.Name
                },
                NextEducationLevels = t.NextEducationLevels
                    .Where(nel => !nel.EducationLevel.DeleteTime.HasValue && !nel.DeleteTime.HasValue)
                    .Select(nel => new EducationLevelResponse
                    {
                        Id = nel.EducationLevel.Id,
                        ExternalId = nel.EducationLevel.ExternalId,
                        Name = nel.EducationLevel.Name
                    })
            }).FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}