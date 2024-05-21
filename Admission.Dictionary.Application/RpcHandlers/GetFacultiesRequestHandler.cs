using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetFacultiesRequestHandler: IRequestHandler<GetFacultiesRequest, IRpcResponse>
{
    private readonly IDictionaryDbContext _context;

    public GetFacultiesRequestHandler(IDictionaryDbContext context)
    {
        _context = context;
    }


    public async Task<IRpcResponse> Handle(GetFacultiesRequest request, CancellationToken cancellationToken)
    {
        if (request.Role == RoleType.Applicant) return new RpcErrorResponse("You have no rights");
        
        var result = await _context.Faculties
            .AsNoTracking()
            .GetUndeleted()
            .Select(f => new FacultyResponse
            {
                Id = f.Id,
                Name = f.Name
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return new FacultiesResponse { Faculties = result };
    }
}