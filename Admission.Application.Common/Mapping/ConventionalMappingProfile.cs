using System.Reflection;
using AutoMapper;

namespace Admission.Application.Common.Mapping;

public sealed class ConventionalMappingProfile: Profile
{
    private static readonly Type MapFromType = typeof(IMapFrom<>);
    
    public ConventionalMappingProfile()
    {
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a =>
            {
                var name = a.GetName().Name;
                return name != null && name.StartsWith("Admission.");
            })
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => !t.IsAbstract &&
                        t.GetInterfaces().Any(ImplementsMapFromInterface));
        
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetInterfaces().FirstOrDefault(ImplementsMapFromInterface)
                ?.GetMethod(nameof(IMapFrom<object>.Mapping));
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
    
    private static bool ImplementsMapFromInterface(Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == MapFromType;
    }
}