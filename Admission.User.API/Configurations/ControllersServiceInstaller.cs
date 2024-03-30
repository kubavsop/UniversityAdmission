using System.Text.Json.Serialization;
using Admission.API.Common;
using Admission.API.Common.ServiceInstaller;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Admission.User.API.Configurations;

public class ControllersServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
            .AddJsonOptions(config => config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}