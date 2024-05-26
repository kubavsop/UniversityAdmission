using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.GetApplicantData;

public sealed class GetApplicantDataRequest: AuthorizedRequest, IRpcRequest<ApplicantDataResponse>
{
    public required Guid ApplicantId { get; init; }
}