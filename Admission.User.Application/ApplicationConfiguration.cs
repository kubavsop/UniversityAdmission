using Admission.User.Application.Services;
using Admission.User.Application.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.User.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}