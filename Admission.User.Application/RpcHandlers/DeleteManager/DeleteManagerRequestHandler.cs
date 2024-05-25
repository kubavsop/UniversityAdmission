using Admission.Application.Common.Extensions;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.DeleteUserRole;
using Admission.User.Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.DeleteManager;

public sealed class DeleteManagerRequestHandler: IRequestHandler<DeleteManagerRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public DeleteManagerRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(DeleteManagerRequest request, CancellationToken cancellationToken)
    {
        if (request.Role != RoleType.Admin && request.UserId == request.Id) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var manager = await _context.Managers
            .Include(u => u.User)
            .GetByIdAsync(request.UserId);

        if (manager == null) return new RpcErrorResponse
        {
            Message = "Manager not found"
        };
        
        manager.ChangeDeleteTime(DateTime.UtcNow);
        
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(
                ur => ur.UserId == request.UserId && ur.Role.Type != RoleType.Applicant, cancellationToken: cancellationToken);
        
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