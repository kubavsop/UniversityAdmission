using Admission.Application.Common.Result;
using Admission.Application.DTOs.Responses;

namespace Admission.Application.Services;

public interface IAdmissionService
{
    Task<Result> CreateAdmissionAsync(Guid userId);
    Task<Result<StudentAdmissionDto>> GetAdmissionAsync(Guid admissionId, Guid userId);
}