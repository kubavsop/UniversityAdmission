using Admission.AdminPanel.Services;
using Admission.AdminPanel.Services.Impl;
using Admission.API.Common.ServiceInstaller;

namespace Admission.AdminPanel.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICookieAuthService, CookieAuthService>();
        services.AddRazorPages().AddRazorRuntimeCompilation();
    }
}