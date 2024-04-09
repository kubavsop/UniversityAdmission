using Admission.API.Common.ServiceInstaller;
using Admission.User.Application.Events.ApplicantCreated;
using Admission.User.Application.Services;
using Admission.User.Application.Services.Impl;
using Admission.User.Domain.Events;

namespace Admission.User.API.Configurations;

public class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicantCreatedEventHandler).Assembly));
    }
}