using Admission.AdminPanel.Services;
using Admission.AdminPanel.Services.Impl;
using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;

namespace Admission.AdminPanel.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapping();
        services.AddScoped<ICookieAuthService, CookieAuthService>();
    }
}