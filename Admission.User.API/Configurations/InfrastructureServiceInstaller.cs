﻿using Admission.API.Common;
using Admission.API.Common.Configuration;
using Admission.Infrastructure.Common.Interceptors;
using Admission.User.Application.Services;
using Admission.User.Domain.Entities;
using Admission.User.Infrastructure;
using Admission.User.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.API.Configurations;

public class UserDbServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<AdmissionUser>()
            .AddRoles<AdmissionRole>()
            .AddEntityFrameworkStores<UserDbContext>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<AuditableEntityInterceptor>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<UserDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}