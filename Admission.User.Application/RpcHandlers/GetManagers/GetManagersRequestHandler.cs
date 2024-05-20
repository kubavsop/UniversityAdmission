using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.GetManagers;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.GetManagers;

public sealed class GetManagersRequestHandler: IRequestHandler<GetManagersRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public GetManagersRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(GetManagersRequest request, CancellationToken cancellationToken)
    {
        if (request.Role < RoleType.SeniorManager) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Type == RoleType.Admin, cancellationToken: cancellationToken);
        var seniorManagerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Type == RoleType.SeniorManager, cancellationToken: cancellationToken);

        var managers = await _context.Managers
            .Where(m => m.User.UserRoles.Any(ur => ur.RoleId != adminRole!.Id))
            .Select(m => new ShortManagerDataResponse
            {
                FullName = m.User.FullName,
                Email = m.User.Email,
                ManagerId = m.Id,
                ManagerRole = m.User.UserRoles.Any(ur => ur.RoleId == seniorManagerRole!.Id)
                    ? RoleType.SeniorManager
                    : RoleType.Manager
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return new ManagersResponse
        {
            Managers = managers
        };
    }
}