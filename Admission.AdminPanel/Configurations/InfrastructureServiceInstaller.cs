using Admission.AdminPanel.Services;
using Admission.AdminPanel.Services.Impl;
using Admission.API.Common.ServiceInstaller;
using Admission.RabbitMQ.Extensions;
using Admission.RabbitMQ.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Admission.AdminPanel.Configurations;

public sealed class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMqConnection(configuration);
        services.AddSingleton<IRpcUserClient, RpcUserClient>();
        services.AddSingleton<IRpcDictionaryMvcClient, RpcDictionaryMvcClient>();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.LogoutPath = new PathString("/Account/Logout");
            });
        services.AddRazorPages().AddRazorRuntimeCompilation();
    }
}