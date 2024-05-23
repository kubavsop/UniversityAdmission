using Admission.API.Common.Extensions;
using Admission.API.Common.ServiceInstaller;
using Admission.User.Infrastructure.Options;

namespace Admission.User.API.Configurations;

public class ControllersServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredControllers();
        services.Configure<AdminOptions>(configuration.GetSection("Admin"));
    }
}