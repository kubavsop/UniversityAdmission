﻿using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.User.Application.Events.ApplicantCreated;
using Admission.User.Application.Options;
using Admission.User.Application.Services;
using Admission.User.Application.Services.Impl;

namespace Admission.User.API.Configurations;

public class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapping();
        services.Configure<RefreshTokenOptions>(configuration.GetSection("RefreshToken"));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IManagerAccessService, ManagerAccessService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicantCreatedEventHandler).Assembly));
    }
}