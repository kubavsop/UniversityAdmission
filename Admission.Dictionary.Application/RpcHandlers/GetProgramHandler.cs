using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.EducationLevel;
using Admission.DTOs.RpcModels.Faculty;
using Admission.DTOs.RpcModels.Program;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetProgramHandler: IRequestHandler<GetProgramRequest, IRpcResponse?>
{
    private readonly IDictionaryDbContext _context;

    public GetProgramHandler(IDictionaryDbContext context)
    {
        _context = context;
    }
    
    public async Task<IRpcResponse?> Handle(GetProgramRequest request, CancellationToken cancellationToken)
    {
        var result = await _context.Programs
            .GetUndeleted()
            .Where(p => p.Id == request.Id)
            .Where(p => !p.EducationLevel.DeleteTime.HasValue && !p.Faculty.DeleteTime.HasValue)
            .Select(p => new ProgramResponse
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Language = p.Language,
                EducationForm = p.EducationForm,
                Faculty = new FacultyResponse
                {
                    Id = p.Faculty.Id,
                    Name = p.Name
                },
                EducationLevel = new EducationLevelResponse
                {
                    Id = p.EducationLevel.Id,
                    ExternalId = p.EducationLevel.ExternalId,
                    Name = p.EducationLevel.Name
                }
            }).FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}