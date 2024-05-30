using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;

public sealed class GetPassportRequest: AuthorizedRequest, IRpcRequest<PassportResponse>
{
    public required Guid ApplicantId { get; init; }
}