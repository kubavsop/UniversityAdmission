using Microsoft.Extensions.DependencyInjection;

namespace Admission.Application.Common.Mapping;

public static class AutoMapperExtension
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ConventionalMappingProfile).Assembly);
        return services;
    }
}