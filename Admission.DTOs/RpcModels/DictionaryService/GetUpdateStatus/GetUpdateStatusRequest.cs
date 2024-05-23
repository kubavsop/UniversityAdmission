using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;

public sealed class GetUpdateStatusRequest: AuthorizedRequest, IRpcRequest<UpdateStatusResponse>;