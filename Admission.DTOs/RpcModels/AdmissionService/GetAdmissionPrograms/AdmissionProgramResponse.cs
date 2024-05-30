namespace Admission.DTOs.RpcModels.AdmissionService.GetAdmissionPrograms;

public class AdmissionProgramResponse
{
    public required Guid Id { get; init; }
    public required int Priority { get; init; }
    public required string EducationProgramName { get; init; }
}