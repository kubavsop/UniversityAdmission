using Admission.DTOs.RpcModels.Enums;

namespace Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

public sealed class UpdateStatusResponse: IRpcResponse
{
    public required StatusInformation FacultyStatusInformation { get; init; }
    public required StatusInformation DocumentTypeStatusInformation { get; init; }
    public required StatusInformation ProgramStatusInformation { get; init; }
    public required StatusInformation EducationLevelStatusInformation { get; init; }
}