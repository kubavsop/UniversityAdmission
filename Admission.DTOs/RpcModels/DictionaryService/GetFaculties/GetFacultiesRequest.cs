using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetFaculties;

public sealed class GetFacultiesRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>;