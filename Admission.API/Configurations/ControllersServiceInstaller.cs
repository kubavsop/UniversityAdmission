using Admission.API.Common.Extensions;
using Admission.API.Common.ServiceInstaller;
using Admission.Application.Options;

namespace Admission.API.Configurations;

public class ControllersServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredControllers();
    }
}