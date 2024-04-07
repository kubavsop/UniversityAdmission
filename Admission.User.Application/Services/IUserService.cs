using Admission.Application.Common.Result;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;

namespace Admission.User.Application.Services;

public interface IUserService
{
    Task<Result<ApplicantDto>> GetApplicantProfileAsync(Guid userId);
    Task<Result> EditApplicantProfileAsync(EditApplicantDto dto, Guid userId);
    Task<Result> EditPasswordAsync(EditPasswordDto dto, Guid userId);
}