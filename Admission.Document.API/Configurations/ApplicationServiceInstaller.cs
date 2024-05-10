using System.Reflection;
using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.Document.Application.IntegrationEvents.Admisson;
using Admission.Document.Application.Services;
using Admission.Document.Application.Services.Impl;

namespace Admission.Document.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IScanService, ScanService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AdmissionCreatedIntegrationEventHandler).Assembly));
        services.AddMapping();
    }
}