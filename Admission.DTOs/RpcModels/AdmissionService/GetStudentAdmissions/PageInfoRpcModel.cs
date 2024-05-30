namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

public sealed class PageInfoRpcModel
{
    public required int Size { get; init; }
    public required int Count { get; init; }
    public required int Current { get; init; }
}