using Admission.DTOs.RpcModels.DocumentType;
using Admission.DTOs.RpcModels.EducationLevel;
using Admission.DTOs.RpcModels.Faculty;
using Admission.DTOs.RpcModels.Program;

namespace Admission.RabbitMQ.Services;

public interface IRpcDictionaryClient
{
    Task<FacultyResponse?> GetFacultyByIdAsync(Guid id);

    Task<ProgramResponse?> GetProgramByIdAsync(Guid id);

    Task<EducationLevelResponse?> GetEducationLevelByIdAsync(int id);

    Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(Guid id);
}