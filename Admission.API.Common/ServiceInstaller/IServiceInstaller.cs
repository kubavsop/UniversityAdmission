using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.API.Common.ServiceInstaller;

public interface IServiceInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}