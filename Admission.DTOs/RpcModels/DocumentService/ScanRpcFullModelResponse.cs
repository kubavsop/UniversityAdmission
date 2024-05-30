namespace Admission.DTOs.RpcModels.DocumentService;

public sealed class ScanRpcFullModelResponse: IRpcResponse
{
    public required Guid ScanId { get; init; }
    public required string Name { get; init; }
    public required string ContentType { get; init; }
    public required byte[] Bytes { get; init; }
}