using Admission.Application.Common.Extensions;
using Admission.Application.Common.Services;
using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.Domain.Entities;
using Admission.DTOs.IntegrationEvents.Events.EducationDocument;
using Admission.DTOs.RpcModels.DictionaryService.GetEducationLevel;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Services.Impl;

public sealed class DocumentService: IDocumentService
{
    private readonly IRpcDictionaryClient _dictionaryClient;
    private readonly IAdmissionDbContext _context;

    public DocumentService(IRpcDictionaryClient dictionaryClient, IAdmissionDbContext context)
    {
        _dictionaryClient = dictionaryClient;
        _context = context;
    }
    
    public async Task CreateEducationDocumentAsync(EducationDocumentCreatedIntegrationEvent integrationEvent)
    {
        await HandleApplicantAdmissionChangedAsync(integrationEvent.UserId);
        await EnsureDocumentTypeSaved(integrationEvent.EducationDocumentTypeId);
        await _context.EducationDocuments.AddAsync(new EducationDocument
        {
            Id = integrationEvent.Id,
            ApplicantId = integrationEvent.UserId,
            EducationDocumentTypeId = integrationEvent.EducationDocumentTypeId,
        });
        
        
        await _context.SaveChangesAsync();
    }

    public Task ChangeDocumentNameAsync(EducationDocumentNameChangedIntegrationEvent integrationEvent)
    {
        return HandleApplicantAdmissionChangedAsync(integrationEvent.UserId);
    }

    public async Task ChangeDocumentTypeAsync(EducationDocumentTypeChangedIntegrationEvent integrationEvent)
    {
        var document = await _context.EducationDocuments.FirstOrDefaultAsync(d => d.Id == integrationEvent.Id);
        if (document == null) return;
        await EnsureDocumentTypeSaved(integrationEvent.EducationDocumentTypeId);
        await HandleApplicantAdmissionChangedAsync(integrationEvent.UserId);
        document.EducationDocumentTypeId = integrationEvent.EducationDocumentTypeId;

        await _context.SaveChangesAsync();
    }

    public async Task ChangeDeleteTimeAsync(EducationDocumentDeleteTimeChangedIntegrationEvent integrationEvent)
    {
        var document = await _context.EducationDocuments.FirstOrDefaultAsync(d => d.Id == integrationEvent.Id);
        if (document == null) return;
        await HandleApplicantAdmissionChangedAsync(integrationEvent.UserId);
        document.DeleteTime = integrationEvent.DeleteTime;
        
        await _context.SaveChangesAsync();
    }

    private async Task EnsureDocumentTypeSaved(Guid typeId)
    {
        var type = await _context.EducationDocumentTypes.FirstOrDefaultAsync(t =>
            t.Id == typeId);

        if (type is { DeleteTime: not null }) return;

        if (type != null) return;

        var documentType = await _dictionaryClient.GetDocumentTypeByIdAsync(typeId);

        if (documentType == null) return;

        var educationLevelsDict = await _context.EducationLevels
            .AsNoTracking()
            .GetUndeleted()
            .ToDictionaryAsync(l => l.ExternalId);

        if (!educationLevelsDict.ContainsKey(documentType.EducationLevel.ExternalId))
        {
            await SaveEducationLevel(documentType.EducationLevel);
        }

        foreach (var educationLevel in documentType.NextEducationLevels)
        {
            if (!educationLevelsDict.ContainsKey(educationLevel.ExternalId))
            {
                await SaveEducationLevel(educationLevel);
            }

            await _context.NextEducationLevels.AddAsync(new NextEducationLevel
            {
                EducationDocumentTypeId = typeId,
                EducationLevelId = educationLevel.ExternalId
            });
        }


        await _context.EducationDocumentTypes.AddAsync(new EducationDocumentType
        {
            Id = documentType.Id,
            Name = documentType.Name,
            EducationLevelId = documentType.EducationLevel.ExternalId,
        });
    }

    private async Task SaveEducationLevel(EducationLevelResponse educationLevelResponse)
    {
        var educationLevel = new EducationLevel
        {
            Id = educationLevelResponse.Id,
            Name = educationLevelResponse.Name,
            ExternalId = educationLevelResponse.ExternalId
        };

        await _context.EducationLevels.AddAsync(educationLevel);
    }
    
    private async Task HandleApplicantAdmissionChangedAsync(Guid userId)
    {
        var currentAdmission = await _context.StudentAdmissions
            .GetUndeleted()
            .Include(a => a.Applicant)
            .FirstOrDefaultAsync(sa => sa.ApplicantId == userId &&
                                       sa.Status != AdmissionStatus.Closed);
        
        if (currentAdmission == null || currentAdmission.Status == AdmissionStatus.Closed) return;
                    
        currentAdmission.ChangeStatus(currentAdmission.ManagerId != null
            ? AdmissionStatus.UnderReview
            : AdmissionStatus.Created);
    }
}