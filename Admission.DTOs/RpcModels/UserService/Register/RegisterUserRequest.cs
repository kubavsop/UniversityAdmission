namespace Admission.DTOs.RpcModels.UserService.Register;

public sealed class RegisterUserRequest: IRpcRequest<RegisterResponse>
{
    public required string FullName { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
}