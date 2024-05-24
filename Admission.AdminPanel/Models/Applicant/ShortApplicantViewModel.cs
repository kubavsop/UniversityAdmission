using Admission.Application.Common.Mapping;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.GetApplicants;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class ShortApplicantViewModel: IMapFrom<ShortApplicantResponse>
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required Guid ApplicantId { get; init; }
    public RoleType? RoleToAdd { get; init; } = null;
}