using Admission.Application.Common.DTOs.Requests;
using Admission.Application.Common.DTOs.Responses;
using Admission.Application.Common.Result;

namespace Admission.User.Application.Services;

public interface IAuthService
{
    public Task<Result<TokenPairDto>> RegisterApplicantAsync(CreateApplicantDto dto);   
    public Task<Result<TokenPairDto>> LoginAsync(LoginCredentialsDto dto);
    public Task<Result<TokenPairDto>> RefreshAsync(RefreshDto dto);
}