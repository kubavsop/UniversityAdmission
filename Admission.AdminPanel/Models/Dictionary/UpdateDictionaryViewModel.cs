using System.Security.Cryptography.X509Certificates;
using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;
using Admission.DTOs.RpcModels.Enums;

namespace Admission.AdminPanel.Models.Dictionary;

public sealed class UpdateDictionaryViewModel: IMapFrom<UpdateStatusResponse>
{
    public UpdateOptions Options { get; set; } = UpdateOptions.All;
    public StatusInformation FacultyStatusInformation { get; init; }
    public StatusInformation DocumentTypeStatusInformation { get; init; }
    public StatusInformation ProgramStatusInformation { get; init; }
    public StatusInformation EducationLevelStatusInformation { get; init; }
}