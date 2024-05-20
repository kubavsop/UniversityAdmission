using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

namespace Admission.Dictionary.Application.Services;

public interface IUpdateStatusService
{
    Task<UpdateStatusResponse> GetUpdateStatusAsync();
    Task SetAllProgressStatus();
    Task SetStatuses();
    Task UpdateStatuses();
    StatusInformation? FacultyStatusInformation { get; set; }
    StatusInformation? DocumentTypeStatusInformation { get; set; }
    StatusInformation? ProgramStatusInformation { get; set; }
    StatusInformation? EducationLevelStatusInformation { get; set; }
}