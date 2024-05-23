namespace Admission.DTOs.RpcModels.UserService.GetApplicants;

public sealed class ApplicantsResponse: IRpcResponse
{
    public required List<ShortApplicantResponse> Applicants { get; init; }
}