using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Application.Services.Impl;

namespace Admission.Dictionary.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDictionaryService, DictionaryService>();
        services.AddScoped<IImporterService, ImporterService>();
        services.AddMapping();
    }
}