using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.DeleteUserRole;
using Admission.User.Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.DeleteUserRole;

public sealed class DeleteUserRoleRequestHandler: IRequestHandler<DeleteUserRoleRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public DeleteUserRoleRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(DeleteUserRoleRequest request, CancellationToken cancellationToken)
    {
        if (request.Role != RoleType.Admin && request.UserId == request.Id) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var userRole = await _context.UserRoles.FirstOrDefaultAsync(
                ur => ur.UserId == request.UserId && ur.Role.Type == request.UserRole, cancellationToken: cancellationToken);
        
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            return new RpcErrorResponse
            {
                Message = "User role not found"
            };
        }

        return new RpcOkResponse();
    }
}