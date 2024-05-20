using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.GetApplicants;

public sealed class GetApplicantsRequest: AuthorizedRequest, IRpcRequest<ApplicantsResponse>;