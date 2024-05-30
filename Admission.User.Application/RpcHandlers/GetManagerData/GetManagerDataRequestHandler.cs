using Admission.Application.Common.Extensions;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;
using Admission.DTOs.RpcModels.UserService.GetManagerData;
using Admission.User.Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.GetManagerData;

public sealed class GetManagerDataRequestHandler: IRequestHandler<GetManagerDataRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public GetManagerDataRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(GetManagerDataRequest request, CancellationToken cancellationToken)
    {
        if (request.Role < RoleType.SeniorManager) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var manager = await _context.Managers
            .Include(m => m.User)
            .Include(m => m.Faculty)
            .GetByIdAsync(request.UserId);

        if (manager == null)
        {
            return new RpcErrorResponse
            {
                Message = "Manager not found"
            };
        }
        
        return new ManagerDataResponse
        {
            ManagerId = manager.Id,
            Email = manager.User.Email,
            Faculty = manager.Faculty == null ? null : new FacultyResponse { Id = manager.Faculty.Id, Name = manager.Faculty.Name },
            FullName = manager.User.FullName
        };
    }
}