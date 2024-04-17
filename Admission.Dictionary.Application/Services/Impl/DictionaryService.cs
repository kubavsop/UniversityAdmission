using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.DTOs.Requests;
using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Domain.Common.Entities;
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
            .Include(t => t.EducationLevels)
            .ToListAsync();
        
        return _mapper.Map<List<EducationDocumentTypeDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationProgramDto>>> GetEducationProgramsAsync(ProgramSearchParameters parameters)
    {
        var facultiesResult = await CheckEntitiesExistenceAsync(
            parameters.Faculties,
            id => _context.Faculties.AnyAsync(f => f.Id == id)
        );
        if (facultiesResult.IsFailure)
        {
            return facultiesResult.Exception;
        }

        var levelsResult = await CheckEntitiesExistenceAsync(
            parameters.EducationLevels,
            id => _context.EducationLevels.AnyAsync(l => l.Id == id));
        if (levelsResult.IsFailure)
        {
            return levelsResult.Exception;
        }

        throw new NotImplementedException();
    }

    private async Task<Result> CheckEntitiesExistenceAsync(
        IEnumerable<Guid> ids,
        Func<Guid, Task<bool>> checkExistence
        ) {
        foreach (var id in ids)
        {
            if (!await checkExistence(id))
            {
                return new NotFoundException("Entity", id);
            } 
        }
        return Result.Success();
    }
}