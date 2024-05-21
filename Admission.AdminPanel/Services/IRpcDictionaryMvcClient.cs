using Admission.Application.Common.Result;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculties;

namespace Admission.AdminPanel.Services;

public interface IRpcDictionaryMvcClient
{
    Task<Result<FacultiesResponse>> GetFaculties(GetFacultiesRequest getFacultiesRequest);
}