using System.Reflection;
using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.API.Common.Configurations;

public sealed class AutoMapperServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ConventionalMappingProfile).Assembly);
    }
}