namespace Admission.DTOs.RpcModels.UserService.GetApplicants;

public sealed class ShortApplicantResponse
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required Guid ApplicantId { get; init; }
}