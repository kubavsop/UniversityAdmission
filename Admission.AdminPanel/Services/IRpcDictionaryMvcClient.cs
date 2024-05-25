using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

namespace Admission.AdminPanel.Services;

public interface IRpcDictionaryMvcClient
{
    Task<Result<FacultiesResponse>> GetFacultiesAsync(GetFacultiesRequest getFacultiesRequest);
    Task<Result<UpdateStatusResponse>> GetUpdateStatusesAsync(GetUpdateStatusRequest getUpdateStatusRequest);
}