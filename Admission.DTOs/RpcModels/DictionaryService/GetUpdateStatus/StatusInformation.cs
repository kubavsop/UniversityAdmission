using Admission.DTOs.RpcModels.Enums;

namespace Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

public sealed class StatusInformation
{
    public DateTime? LastUpdate { get; set; }
    public UpdateStatus UpdateStatus { get; set; }
}