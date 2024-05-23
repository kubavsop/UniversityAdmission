using Admission.DTOs.RpcModels.DictionaryService.GetDocumentType;
using Admission.DTOs.RpcModels.DictionaryService.GetEducationLevel;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;
using Admission.DTOs.RpcModels.DictionaryService.GetProgram;

namespace Admission.Application.Common.Services;

public interface IRpcDictionaryClient
{
    Task<FacultyResponse?> GetFacultyByIdAsync(Guid id);

    Task<ProgramResponse?> GetProgramByIdAsync(Guid id);

    Task<EducationLevelResponse?> GetEducationLevelByIdAsync(int id);

    Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(Guid id);
}