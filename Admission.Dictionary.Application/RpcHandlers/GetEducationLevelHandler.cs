using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.DictionaryService.GetEducationLevel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetEducationLevelHandler: IRequestHandler<GetEducationLevelRequest, IRpcResponse?>
{
    private readonly IDictionaryDbContext _context;

    public GetEducationLevelHandler(IDictionaryDbContext context)
    {
        _context = context;
    }
    
    public async Task<IRpcResponse?> Handle(GetEducationLevelRequest request, CancellationToken cancellationToken)
    {
        var level = await _context.EducationLevels.GetUndeleted().FirstOrDefaultAsync(l => l.ExternalId == request.Id, cancellationToken: cancellationToken);
        if (level == null) return null;

        return new EducationLevelResponse
        {
            Id = level.Id,
            ExternalId = level.ExternalId,
            Name = level.Name
        };
    }
}