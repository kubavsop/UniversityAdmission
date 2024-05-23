using Admission.API.Common.ServiceInstaller;
using Admission.Application.Common.Mapping;
using Admission.Application.IntegrationEvents.Applicant;
using Admission.Application.Options;
using Admission.Application.Services;
using Admission.Application.Services.Impl;

namespace Admission.API.Configurations;

public sealed class ApplicationServiceInstaller: IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProgramService, ProgramService>();
        services.AddScoped<IAdmissionService, AdmissionService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IIntegrationAdmissionService, IntegrationAdmissionService>();
        services.Configure<MaximumNumberOfApplicantPrograms>(configuration.GetSection("MaximumNumberOfApplicantPrograms"));
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicantChangedIntegrationEventHandler).Assembly));
        services.AddMapping();
    }
}