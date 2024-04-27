using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.User.Application.Events.ApplicantCreated;
using Admission.User.Application.Services;
using Admission.User.Application.Services.Impl;

namespace Admission.User.API.Configurations;

public class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapping();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicantCreatedEventHandler).Assembly));
    }
}