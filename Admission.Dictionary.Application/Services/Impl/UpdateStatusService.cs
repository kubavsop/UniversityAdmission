using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Entities;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;
using Admission.DTOs.RpcModels.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Admission.Dictionary.Application.Services.Impl;

public class UpdateStatusService: IUpdateStatusService
{
    private readonly IServiceProvider _provider;

    public UpdateStatusService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public StatusInformation? FacultyStatusInformation { get; set; }
    public StatusInformation? DocumentTypeStatusInformation { get; set; }
    public StatusInformation? ProgramStatusInformation { get; set; }
    public StatusInformation? EducationLevelStatusInformation { get; set; }
    
    public async Task<UpdateStatusResponse> GetUpdateStatusAsync()
    {
        await SetStatuses();

        return new UpdateStatusResponse
        {
            FacultyStatusInformation = FacultyStatusInformation!,
            DocumentTypeStatusInformation = DocumentTypeStatusInformation!,
            EducationLevelStatusInformation = EducationLevelStatusInformation!,
            ProgramStatusInformation = ProgramStatusInformation!
        };
    }

    public async Task SetAllProgressStatus()
    {
        await SetStatuses();

        FacultyStatusInformation!.UpdateStatus = UpdateStatus.ProcessOfUpdating;
        DocumentTypeStatusInformation!.UpdateStatus = UpdateStatus.ProcessOfUpdating;
        EducationLevelStatusInformation!.UpdateStatus = UpdateStatus.ProcessOfUpdating;
        ProgramStatusInformation!.UpdateStatus = UpdateStatus.ProcessOfUpdating;
    }

    public async Task SetStatuses()
    {
        if (FacultyStatusInformation != null &&
            DocumentTypeStatusInformation != null &&
            ProgramStatusInformation != null &&
            EducationLevelStatusInformation != null) return;
        
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IDictionaryDbContext>();
        
        FacultyStatusInformation ??= await GetBaseEntityStatusInformation<Faculty>(context);
        DocumentTypeStatusInformation ??= await GetBaseEntityStatusInformation<EducationDocumentType>(context);
        ProgramStatusInformation ??= await GetBaseEntityStatusInformation<EducationProgram>(context);
        EducationLevelStatusInformation ??= await GetBaseEntityStatusInformation<EducationLevel>(context);
    }

    public async Task UpdateStatuses()
    {
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IDictionaryDbContext>();
        
        FacultyStatusInformation = await GetBaseEntityStatusInformation<Faculty>(context);
        DocumentTypeStatusInformation = await GetBaseEntityStatusInformation<EducationDocumentType>(context);
        ProgramStatusInformation = await GetBaseEntityStatusInformation<EducationProgram>(context);
        EducationLevelStatusInformation = await GetBaseEntityStatusInformation<EducationLevel>(context);
    }

    private async Task<StatusInformation> GetBaseEntityStatusInformation<T>(IDictionaryDbContext context) where T: BaseEntity
    {
        var lastUpdate = await context.Set<T>()
            .Where(e => !e.DeleteTime.HasValue)
            .Select(e => new { CreateTime = e.CreateTime, ModifiedTime = e.ModifiedTime })
            .Select(e =>
                e.ModifiedTime.HasValue
                    ? (e.ModifiedTime > e.CreateTime ? e.ModifiedTime : e.CreateTime)
                    : e.CreateTime)
            .MaxAsync();
        
        return new StatusInformation
        {
            LastUpdate = lastUpdate,
            UpdateStatus = lastUpdate.HasValue ? UpdateStatus.Updated : UpdateStatus.NotUpdated
        };
    }
}