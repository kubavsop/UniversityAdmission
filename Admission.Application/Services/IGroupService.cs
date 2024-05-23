using Admission.Application.Common.Result;
using Admission.Application.DTOs.Responses;

namespace Admission.Application.Services;

public interface IGroupService
{
    Task<Result<IEnumerable<AdmissionGroupDto>>> GetGroupsAsync(Guid userId);
}