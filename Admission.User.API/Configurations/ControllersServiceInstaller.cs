using Admission.API.Common.Extensions;
using Admission.API.Common.ServiceInstaller;

namespace Admission.User.API.Configurations;

public class ControllersServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredControllers();
    }
}