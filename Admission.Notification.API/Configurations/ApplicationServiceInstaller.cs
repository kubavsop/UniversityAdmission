using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Messaging.IntegrationEvent.Events.Email;
using Admission.Notification.Application.Events;

namespace Admission.Notification.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(MailRequestIntegrationEventHandler).Assembly));
    }
}