using Admission.Application.Common.Result;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Requests;
using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Dictionary.Domain.Entities;
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
            .ToListAsync();
        
        return _mapper.Map<List<FacultyDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationLevelDto>>> GetEducationLevelsAsync()
    {
        var result = await _context.EducationLevels
            .AsNoTracking()
            .ToListAsync();
        
        return _mapper.Map<List<EducationLevelDto>>(result);
    }

    public async Task<Result<IEnumerable<EducationDocumentTypeDto>>> GetDocumentTypesAsync()
    {
        var result = await _context.DocumentTypes
            .AsNoTracking()
            .Include(t => t.EducationLevel)
            .Include(t => t.NextEducationLevels)
            .ToListAsync();
        
        return _mapper.Map<List<EducationDocumentTypeDto>>(result);
    }

    public Task<Result<IEnumerable<EducationProgramDto>>> GetEducationProgramsAsync(ProgramSearchParameters parameters)
    {
        throw new NotImplementedException();
    }
}