using System.Text.Json.Serialization;
using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.API.Common.ServiceInstaller;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Admission.User.API.Configurations;

public class ControllersServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredControllers();
    }
}