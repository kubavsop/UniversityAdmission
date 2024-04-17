using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.DTOs.Requests;
using Admission.Dictionary.Application.DTOs.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.Services.Impl;

public class DictionaryService: IDictionaryService
{
    private readonly IDictionaryDbContext _context;
    private readonly IMapper _mapper;

    public DictionaryService(IDictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<FacultyDto>>> GetFacultiesAsync()
    { 
        var result = await _context.Faculties
            .AsNoTracking()
            .GetUndeleted()
            .ToListAsync();
        
        return _mapper.Map<List<FacultyDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationLevelDto>>> GetEducationLevelsAsync()
    {
        var result = await _context.EducationLevels
            .AsNoTracking()
            .GetUndeleted()
            .ToListAsync();
        
        return _mapper.Map<List<EducationLevelDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationDocumentTypeDto>>> GetDocumentTypesAsync()
    {
        var result = await _context.DocumentTypes
            .AsNoTracking()
            .GetUndeleted()
            .Include(t => t.EducationLevel)
            .Where(d => !d.EducationLevel.DeleteTime.HasValue)
            .Include(t => t.NextEducationLevels
                .Where(nel => !nel.DeleteTime.HasValue))
            .ThenInclude(nel => nel.EducationLevel)
            .ToListAsync();
        
        return _mapper.Map<List<EducationDocumentTypeDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationProgramDto>>> GetEducationProgramsAsync(ProgramSearchParameters parameters)
    {
        var facultiesResult = await CheckEntitiesExistenceAsync(
            parameters.Faculties,
            id => _context.Faculties.AnyAsync(f => f.Id == id));
        if (facultiesResult.IsFailure)
        {
            return facultiesResult.Exception;
        }

        var levelsResult = await CheckEntitiesExistenceAsync(
            parameters.EducationLevels,
            id => _context.EducationLevels.AnyAsync(l => l.ExternalId == id));
        if (levelsResult.IsFailure)
        {
            return levelsResult.Exception;
        }

        var queryable = _context.Programs
            .Where(p => parameters.Faculties.Count == 0 || parameters.Faculties.Any(id => id == p.FacultyId))
            .Where(p => parameters.EducationLevels.Count == 0 ||
                        parameters.EducationLevels.Any(id => id == p.EducationLevelId));

        throw new NotImplementedException();
    }

    private async Task<Result> CheckEntitiesExistenceAsync<TId>(
        IEnumerable<TId> ids,
        Func<TId, Task<bool>> checkExistence
        ) {
        foreach (var id in ids)
        {
            if (!await checkExistence(id))
            {
                return new NotFoundException($"Entity with id={id} was not found.");
            } 
        }
        return Result.Success();
    }
}