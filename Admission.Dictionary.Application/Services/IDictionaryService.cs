using Admission.Application.Common.Result;
using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Requests;
using Admission.Dictionary.Application.DTOs.Responses;

namespace Admission.Dictionary.Application.Services;

public interface IDictionaryService
{ 
    Task<Result<IEnumerable<FacultyDto>>> GetFacultiesAsync();
    Task<Result<IEnumerable<EducationLevelDto>>> GetEducationLevelsAsync();
    Task<Result<IEnumerable<EducationDocumentTypeDto>>> GetDocumentTypesAsync();
    Task<Result<IEnumerable<EducationProgramDto>>> GetEducationProgramsAsync(ProgramSearchParameters parameters);
}