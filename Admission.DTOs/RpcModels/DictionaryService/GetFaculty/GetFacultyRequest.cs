using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetFaculty;

public sealed class GetFacultyRequest: BaseDto, IRpcRequest<FacultyResponse?>;