using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;

public sealed class GetAdmissionGroupsRequest: AuthorizedRequest, IRpcRequest<AdmissionGroupsResponse>;