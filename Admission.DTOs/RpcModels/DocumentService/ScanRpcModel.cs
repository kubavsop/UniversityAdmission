namespace Admission.DTOs.RpcModels.DocumentService;

public sealed class ScanRpcModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ContentType { get; init; }
    public required byte[] Bytes { get; init; }
}