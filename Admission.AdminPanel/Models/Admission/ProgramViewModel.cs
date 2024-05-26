using Admission.Application.Common.Mapping;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;

namespace Admission.AdminPanel.Models.Admission;

public sealed class ProgramViewModel: IMapFrom<AdmissionProgramsResponse>
{
    public Guid StudentAdmissionId { get; set; }
    public required List<AdmissionProgramResponse> Programs { get; init; }
    
    public required bool IsEditable { get; init; }
}