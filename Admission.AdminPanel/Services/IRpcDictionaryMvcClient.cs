using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

namespace Admission.AdminPanel.Services;

public interface IRpcDictionaryMvcClient
{
    Task<Result<FacultiesResponse>> GetFaculties(GetFacultiesRequest getFacultiesRequest);
    Task<Result<UpdateStatusResponse>> GetUpdateStatuses(GetUpdateStatusRequest getUpdateStatusRequest);
}