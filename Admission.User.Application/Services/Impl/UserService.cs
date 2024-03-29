using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admission.User.Application.Services.Impl;

public sealed class UserService: IUserService
{
    private readonly UserManager<AdmissionUser> _userManager;

    public UserService(UserManager<AdmissionUser> userManager)
    {
        _userManager = userManager;
    }
    
    
}