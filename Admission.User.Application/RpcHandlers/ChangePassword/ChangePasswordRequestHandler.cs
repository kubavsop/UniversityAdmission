using Admission.Application.Common.Extensions;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.ChangePassword;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Application.RpcHandlers.ChangePassword;

public sealed class ChangePasswordRequestHandler: IRequestHandler<ChangePasswordRequest, IRpcResponse>
{
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly IUserDbContext _context;

    public ChangePasswordRequestHandler(UserManager<AdmissionUser> userManager, IUserDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }


    public async Task<IRpcResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        if (request.NewPassword == request.OldPassword)
        {
            return new RpcErrorResponse
            {
                Message = "Passwords must be different"
            };
        }

        var user = await _context.Users.GetByIdAsync(request.Id);
        if (user == null)
        {
            return new RpcErrorResponse
            {
                Message = "User not found"
            };
        }
        
        var identityResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        return identityResult.Succeeded ? new RpcOkResponse() : new RpcErrorResponse
        {
            Message = string.Join("; ", identityResult.Errors.Select(e => $"{e.Code}: {e.Description}"))
        };    
    }
}