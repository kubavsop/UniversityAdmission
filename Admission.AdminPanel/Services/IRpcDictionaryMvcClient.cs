using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetDocumentType;
using Admission.DTOs.RpcModels.DictionaryService.GetDocumentTypes;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

namespace Admission.AdminPanel.Services;

public interface IRpcDictionaryMvcClient
{
    Task<Result<FacultiesResponse>> GetFacultiesAsync(GetFacultiesRequest getFacultiesRequest);
    Task<Result<UpdateStatusResponse>> GetUpdateStatusesAsync(GetUpdateStatusRequest getUpdateStatusRequest);
    Task<Result<DocumentTypesResponse>> GetDocumentTypesAsync(GetDocumentTypesRequest documentTypesRequest);
}