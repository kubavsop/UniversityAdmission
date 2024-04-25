using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.Faculty;

public sealed class GetFacultyRequest: BaseDto, IRpcRequest<FacultyResponse?>;