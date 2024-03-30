using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.API.Common.ServiceInstaller;

public static class ServiceInstallerExtension
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in serviceInstallers)
        {
            installer.Install(services, configuration);
        }
        
        return services;
    }
    
    private static bool IsAssignableToType<T>(Type type) =>
        typeof(T).IsAssignableFrom(type) && !type.IsAbstract;
}