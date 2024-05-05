using System.Reflection;
using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;

namespace Admission.Document.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddMapping();
    }
}