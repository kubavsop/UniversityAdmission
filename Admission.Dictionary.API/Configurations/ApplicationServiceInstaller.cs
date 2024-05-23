using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Application.Events.DocumentType;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Application.Services.Impl;

namespace Admission.Dictionary.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUpdateStatusService, UpdateStatusService>();
        services.AddScoped<IDictionaryService, DictionaryService>();
        services.AddScoped<IImporterService, ImporterService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DocumentNameChangedEventHandler).Assembly));
        services.AddMapping();
    }
}