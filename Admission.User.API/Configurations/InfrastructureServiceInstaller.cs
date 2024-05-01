using Admission.API.Common.ServiceInstaller;
using Admission.Infrastructure.Common.Interceptors;
using Admission.JWT;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.Extensions;
using Admission.OutboxMessages.Interceptors;
using Admission.RabbitMQ.Extensions;
using Admission.User.Application.Context;
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
            .AddJwtAuthentication()
            .AddRabbitMqConnection(configuration)
            .AddProducer()
            .AddRpcDictionaryClient(configuration)
            .AddOutboxMessages();
        
        services.AddScoped<IUserDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        
        services
            .AddIdentityCore<AdmissionUser>()
            .AddRoles<AdmissionRole>()
            .AddEntityFrameworkStores<UserDbContext>();
        
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<AuditableEntityInterceptor>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<UserDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}