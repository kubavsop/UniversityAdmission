using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;

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

    public async Task<ProgramPagedListDto> GetTest()
    {
        return await _externalDictionaryService.GetProgramsAsync();
    }
}