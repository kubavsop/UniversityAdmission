using Admission.Application.Common.Result;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;

namespace Admission.User.Application.Services;

public interface IAuthService
{
    Task<Result<TokenPairDto>> RegisterApplicantAsync(CreateApplicantDto dto);   
    Task<Result<TokenPairDto>> LoginAsync(LoginCredentialsDto dto);
    Task<Result<TokenPairDto>> RefreshAsync(RefreshDto dto);
    Task<Result> LogoutAsync(Guid userId);
}