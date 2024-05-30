namespace Admission.DTOs.RpcModels.DocumentService;

public sealed class ScanRpcModel
{
    public required Guid ScanId { get; init; }
    public required string Name { get; init; }
    public required bool IsEditable { get; init; }
}