using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;

namespace Admission.Dictionary.Application.Services;

public interface IExternalDictionaryService
{
    Task<IEnumerable<FacultyDto>> GetFacultiesAsync();
    Task<IEnumerable<EducationLevelDto>> GetEducationLevelsAsync();
    Task<IEnumerable<EducationDocumentTypeDto>> GetDocumentTypesAsync();
    Task<ProgramPagedListDto> GetProgramsAsync(int page = 1, int size = 10);
}