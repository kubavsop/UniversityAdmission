using Admission.API.Common;
using Admission.API.Common.Configuration;
using Admission.User.Application.Services;
using Admission.User.Application.Services.Impl;

namespace Admission.User.API.Configurations;

public class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
    }
}