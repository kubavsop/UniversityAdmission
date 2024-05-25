using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.CreateAdmissionGroup;

public sealed class CreateAdmissionGroupRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>;