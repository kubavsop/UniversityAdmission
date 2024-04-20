using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;

namespace Admission.Dictionary.Application.Services;

public interface IExternalDictionaryService
{
    Task<ICollection<FacultyDto>> GetFacultiesAsync();
    Task<ICollection<EducationLevelDto>> GetEducationLevelsAsync();
    Task<ICollection<EducationDocumentTypeDto>> GetDocumentTypesAsync();
    Task<ProgramPagedListDto> GetProgramsAsync(int page = 1, int size = 10);
}