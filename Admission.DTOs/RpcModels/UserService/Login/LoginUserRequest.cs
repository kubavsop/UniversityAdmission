namespace Admission.DTOs.RpcModels.UserService.Login;

public sealed class LoginUserRequest: IRpcRequest<LoginResponse>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}