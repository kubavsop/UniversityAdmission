using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;

namespace Admission.DTOs.RpcModels.UserService.GetManagerData;

public sealed class ManagerDataResponse: IRpcResponse
{
    public required Guid ManagerId { get; init; }
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public FacultyResponse? Faculty { get; init; }
}