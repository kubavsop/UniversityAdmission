using Admission.API.Common.ServiceInstaller;
using Admission.Document.Application.Context;
using Admission.Document.Application.Services;
using Admission.Document.Infrastructure;
using Admission.Document.Infrastructure.Services;
using Admission.Infrastructure.Common.Interceptors;
using Admission.JWT;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.Extensions;
using Admission.OutboxMessages.Interceptors;
using Admission.RabbitMQ.Extensions;
using Microsoft.EntityFrameworkCore;
using FileOptions = Admission.Document.Infrastructure.Options.FileOptions;

namespace Admission.Document.API.Configurations;

public class InfrastructureServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRabbitMqConnection(configuration)
            .AddProducer()
            .AddConsumer()
            .AddRpcConsumer(configuration)
            .AddRpcDictionaryClient(configuration)
            .AddJwtAuthentication()
            .AddOutboxMessages();

        services.Configure<FileOptions>(configuration.GetSection("FilePath"));
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddScoped<IDocumentDbContext>(provider => provider.GetRequiredService<DocumentDbContext>());
        services.AddScoped<IOutboxMessageDbContext>(provider => provider.GetRequiredService<DocumentDbContext>());
        services.AddScoped<IFileProvider, FileProvider>();   
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DocumentDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>());
                options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
                options.UseNpgsql(connectionString);
            });
    }
}