using Admission.API.Common.Extensions;
using Admission.API.Common.ServiceInstaller;

namespace Admission.Dictionary.API.Configurations;

public class ControllersInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredControllers();
    }
}