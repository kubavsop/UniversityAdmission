using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.ChangeUserRole;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.AddUserRole;

public sealed class AddUserRoleRequestHandler: IRequestHandler<AddUserRoleRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public AddUserRoleRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(AddUserRoleRequest request, CancellationToken cancellationToken)
    {
        if (request.Role != RoleType.Admin) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var applicantRole = await _context.Roles.FirstOrDefaultAsync(r => r.Type == RoleType.Applicant, cancellationToken: cancellationToken);
        var roleToAdd = await _context.Roles.FirstOrDefaultAsync(r => r.Type == request.UserRole, cancellationToken: cancellationToken);
        if (roleToAdd == null || applicantRole == null) return new RpcErrorResponse
        {
            Message = "Role was not found"
        };

        if (request.UserRole == RoleType.Applicant) return new RpcErrorResponse
        {
            Message = "Role has already been added"
        };

        var user = await _context
            .Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        
        if (user == null) return new RpcErrorResponse
        {
            Message = "User not found"
        };

        foreach (var userRole in user.UserRoles)
        {
            if (userRole.RoleId != applicantRole.Id)
            {
                userRole.RoleId = roleToAdd.Id;
                await _context.SaveChangesAsync(cancellationToken);
                return new RpcOkResponse();
            }
        }

        await _context.Managers.AddAsync(Manager.Create(user), cancellationToken);
        
        await _context.UserRoles.AddAsync(new AdmissionUserRole
        {
            UserId = request.UserId,
            RoleId = roleToAdd.Id
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return new RpcOkResponse();
    }
}