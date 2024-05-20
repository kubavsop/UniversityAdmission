using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.Register;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Application.RpcHandlers.Register;

public sealed class RegisterRequestHandler: IRequestHandler<RegisterUserRequest, IRpcResponse>
{
    private readonly UserManager<AdmissionUser> _userManager;

    public RegisterRequestHandler(UserManager<AdmissionUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<IRpcResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var defaultRole = RoleType.Applicant.ToString();
        var user = new AdmissionUser
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new RpcErrorResponse
            {
                Message = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"))
            };
        }

        await _userManager.AddToRoleAsync(user, defaultRole);

        return new RegisterResponse
        {
            UserId = user.Id,
            RoleType = RoleType.Applicant
        };
    }
}