using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.UserService.GetApplicants;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class ApplicantsViewModel: IMapFrom<ApplicantsResponse>
{
    public required List<ShortApplicantViewModel> Applicants { get; init; }
}