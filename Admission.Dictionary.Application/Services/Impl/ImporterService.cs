using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.Services.Impl;

public class ImporterService: IImporterService
{
    private readonly IExternalDictionaryService _externalDictionaryService;
    private readonly IDictionaryDbContext _context;
    
    public ImporterService(IExternalDictionaryService externalDictionaryService, IDictionaryDbContext context)
    {
        _externalDictionaryService = externalDictionaryService;
        _context = context;
    }

    public async Task TestUpdate()
    {
        await UpdateFaculties();
    }
    
    
    public async Task UpdateFaculties() => await UpdateEntitiesAsync(
        await _context.Faculties.GetUndeleted().ToDictionaryAsync(f => f.Id),
        await _externalDictionaryService.GetFacultiesAsync(),
        (faculty, dto) => faculty.Name != dto.Name,
        (faculty, dto) => faculty.Name = dto.Name,
        dto => new Faculty { Id = dto.Id, Name = dto.Name },
        dto => dto.Id);

    public Task UpdatePrograms()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateEducationLevels() => await UpdateEntitiesAsync(
        await _context.EducationLevels.GetUndeleted().ToDictionaryAsync(f => f.ExternalId),
        await _externalDictionaryService.GetEducationLevelsAsync(),
        (level, dto) => level.Name != dto.Name,
        (level, dto) => level.Name = dto.Name,
        dto => new EducationLevel { Id = Guid.NewGuid(), ExternalId = dto.Id, Name = dto.Name },
        dto => dto.Id);

    public Task UpdateDocumentTypes()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateEntitiesAsync<TEntity, TDto, TKey>(
        Dictionary<TKey, TEntity> dict,
        IEnumerable<TDto> dtos,
        Func<TEntity, TDto, bool> checkChange,
        Action<TEntity, TDto> updateEntityAsync,
        Func<TDto, TEntity> createEntity,
        Func<TDto, TKey> getDtoId,
        Func<TDto, bool>? isEntityAddable = null) where TKey : notnull where TEntity : BaseEntity
    {
        foreach (var dto in dtos)
        {
            dict.TryGetValue(getDtoId(dto), out var entity);
            if (entity != null)
            {
                if (checkChange(entity, dto))
                {
                    updateEntityAsync(entity, dto);
                }
            }
            else if (isEntityAddable == null || isEntityAddable(dto))
            {
                await _context.AddAsync(createEntity(dto));
            }
        }

        await _context.SaveChangesAsync();
    }
}