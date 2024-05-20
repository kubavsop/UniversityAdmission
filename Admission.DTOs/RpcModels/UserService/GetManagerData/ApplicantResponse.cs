namespace Admission.DTOs.RpcModels.UserService.GetManagerData;

public sealed class ApplicantResponse
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
}