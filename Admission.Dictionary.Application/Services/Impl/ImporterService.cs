using Admission.Application.Common.Extensions;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Dictionary.Domain.Entities;
using Admission.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.Services.Impl;

public class ImporterService : IImporterService
{
    private readonly IExternalDictionaryService _externalDictionaryService;
    private readonly IDictionaryDbContext _context;

    private IEnumerable<EducationLevel> _educationLevels;
    private IEnumerable<Faculty> _faculties;

    public ImporterService(IExternalDictionaryService externalDictionaryService, IDictionaryDbContext context)
    {
        _externalDictionaryService = externalDictionaryService;
        _context = context;
    }

    public async Task TestUpdate()
    {
        await UpdateFaculties();
        await UpdateDocumentTypes();
        await UpdatePrograms();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFaculties()
    {
        var dtos = await _externalDictionaryService.GetFacultiesAsync();
        var entities = await _context.Faculties.ToListAsync();

        _faculties = dtos.Select(dto => new Faculty { Id = dto.Id, Name = dto.Name });

        await UpdateEntitiesAsync(
            entities.ToDictionary(f => f.Id),
            dtos,
            entities.Select(e => e.Id).Except(dtos.Select(d => d.Id)),
            (faculty, dto) =>
            {
                faculty.Name = dto.Name;
                faculty.ChangeDeleteTime(null);
            },
            dto => new Faculty { Id = dto.Id, Name = dto.Name },
            dto => dto.Id);
    }

    public async Task UpdatePrograms()
    {
        _educationLevels ??= await _context.EducationLevels
            .AsNoTracking()
            .GetUndeleted()
            .ToListAsync();

        _faculties ??= await _context.Faculties
            .AsNoTracking()
            .GetUndeleted()
            .ToListAsync();

        const int currentSize = 100;
        var currentPage = 1;
        
        var entities = await _context.Programs.ToListAsync();
        var dictionary = entities.ToDictionary(e => e.Id);
        var entitiesId = entities.Select(e => e.Id).ToList();
        IEnumerable<Guid> toDeleteIds = entitiesId; 
        ProgramPagedListDto pageInfoDto;
        do
        {
            pageInfoDto = await _externalDictionaryService.GetProgramsAsync(currentPage, currentSize);
            toDeleteIds = toDeleteIds.Except(pageInfoDto.Programs.Select(p => p.Id));
            await UpdateEntitiesAsync(
                dictionary,
                pageInfoDto.Programs,
                [],
                UpdateProgram,
                CreateProgram,
                dto => dto.Id,
                IsProgramAddable
            );
            currentPage++;
        } while (pageInfoDto.Pagination.Count >= currentPage);

        foreach (var id in toDeleteIds)
        {
            dictionary[id].ChangeDeleteTime(DateTime.UtcNow);
        }
    }

    public async Task UpdateEducationLevels()
    {
        var dtos = await _externalDictionaryService.GetEducationLevelsAsync();
        var entities = await _context.EducationLevels.ToListAsync();

        _educationLevels = dtos.Select(dto => new EducationLevel { ExternalId = dto.Id, Name = dto.Name }).ToList();

        await UpdateEntitiesAsync(
            entities.ToDictionary(l => l.ExternalId),
            dtos,
            entities.Select(e => e.ExternalId).Except(dtos.Select(d => d.Id)),
            (level, dto) =>
            {
                level.Name = dto.Name;
                level.ChangeDeleteTime(null);
            },
            dto => new EducationLevel { Id = Guid.NewGuid(), ExternalId = dto.Id, Name = dto.Name },
            dto => dto.Id);
    }

    public async Task UpdateDocumentTypes()
    {
        await UpdateEducationLevels();

        var dtos = await _externalDictionaryService.GetDocumentTypesAsync();
        var entities = await _context.DocumentTypes.Include(t => t.NextEducationLevels).ToListAsync();

        await UpdateEntitiesAsync(
            entities.ToDictionary(t => t.Id),
            dtos,
            entities.Select(e => e.Id).Except(dtos.Select(d => d.Id)),
            UpdateDocumentType,
            dto => new EducationDocumentType (dto.Id, dto.Name, dto.EducationLevel.Id ),
            dto => dto.Id);


        var nextEducationLevelsEntities = entities.SelectMany(e => e.NextEducationLevels).ToList();
        var nextEducationLevelsDtos = dtos.SelectMany(e => e.NextEducationLevels
            .Select(nel => new NextEducationLevel { EducationDocumentTypeId = e.Id, EducationLevelId = nel.Id })
        ).ToList();

        await UpdateEntitiesAsync(
            nextEducationLevelsEntities.ToDictionary(e => e),
            nextEducationLevelsDtos,
            nextEducationLevelsEntities.Except(nextEducationLevelsDtos),
            (entity, dto) =>
            {
                entity.ChangeDeleteTime(null);
            },
            dto => dto,
            dto => dto);
    }


    private async Task UpdateEntitiesAsync<TEntity, TDto, TKey>(
        Dictionary<TKey, TEntity> dict,
        IEnumerable<TDto> dtos,
        IEnumerable<TKey> deleteEntities,
        Action<TEntity, TDto> updateEntityAsync,
        Func<TDto, TEntity> createEntity,
        Func<TDto, TKey> getDtoId,
        Predicate<TDto>? isEntityAddable = null) where TKey : notnull where TEntity : AggregateRoot
    {
        foreach (var dto in dtos)
        {
            dict.TryGetValue(getDtoId(dto), out var entity);
            if (entity != null)
            {
                updateEntityAsync(entity, dto);
            }
            else if (isEntityAddable == null || isEntityAddable(dto))
            {
                await _context.AddAsync(createEntity(dto));
            }
        }

        foreach (var entity in deleteEntities)
        {
            dict[entity].ChangeDeleteTime(DateTime.UtcNow);
        }
    }

    private static void UpdateDocumentType(EducationDocumentType type, EducationDocumentTypeDto dto)
    {
        type.ChangeName(dto.Name);
        type.ChangeDeleteTime(null);
        type.ChangedEducationLevel(new EducationLevel { ExternalId = dto.EducationLevel.Id, Name = dto.EducationLevel.Name });
    }

    private void UpdateProgram(EducationProgram program, EducationProgramDto dto)
    {
        program.Name = dto.Name;
        program.Code = dto.Code;
        program.Language = dto.Language;
        program.EducationForm = dto.EducationForm;

        program.ChangeDeleteTime(null);

        if (program.FacultyId != dto.Faculty.Id && _faculties.Any(f => f.Id == dto.Faculty.Id))
        {
            program.FacultyId = dto.Faculty.Id;
        }
        
        if (program.EducationLevelId != dto.EducationLevel.Id && _educationLevels.Any(l => l.ExternalId == dto.EducationLevel.Id))
        {
            program.EducationLevelId = dto.EducationLevel.Id;
        }
    }

    private static EducationProgram CreateProgram(EducationProgramDto dto) => new EducationProgram
    {
        Id = dto.Id,
        Name = dto.Name,
        Code = dto.Code,
        Language = dto.Language,
        EducationForm = dto.EducationForm,
        FacultyId = dto.Faculty.Id,
        EducationLevelId = dto.EducationLevel.Id
    };

    private bool IsProgramAddable(EducationProgramDto dto) =>_educationLevels.Any(l => l.ExternalId == dto.EducationLevel.Id) &&
               _faculties.Any(f => f.Id == dto.Faculty.Id);
}