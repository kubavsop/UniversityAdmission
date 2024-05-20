using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.Login;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.Login;

public sealed class LoginRequestHandler: IRequestHandler<LoginUserRequest, IRpcResponse>
{
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly IUserDbContext _context;

    public LoginRequestHandler(UserManager<AdmissionUser> userManager, IUserDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Email);
        if (user is not { DeleteTime: null } || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return new RpcErrorResponse
            {
                Message = "Invalid credentials" 
            };
        }

        return new LoginResponse
        {
            UserId = user.Id,
            RoleType = await _context.UserRoles.Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Type)
                .MaxAsync(cancellationToken: cancellationToken)
        };
    }
}