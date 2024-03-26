using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Admission.User.API;

public static class PresentationConfiguration
{
    public static void AddPresentationLayer(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
            .AddJsonOptions(config => config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
}