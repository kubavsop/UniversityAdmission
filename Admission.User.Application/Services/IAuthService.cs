using Admission.Application.Common.DTOs.Requests;
using Admission.Application.Common.DTOs.Responses;
using Admission.Application.Common.Result;

namespace Admission.User.Application.Services;

public interface IAuthService
{
    public Task<Result<TokenPairDto>> RegisterApplicant(CreateApplicantDto dto);   
    public Task<Result<TokenPairDto>> Login(LoginCredentialsDto dto);
    public Task<Result<TokenPairDto>> Refresh(RefreshDto dto);
}