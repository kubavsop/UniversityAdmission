namespace Admission.DTOs.RpcModels.DocumentService;

public class ScanRpcFullModelRequest
{
    public required string Name { get; init; }
    public required string ContentType { get; init; }
    public required byte[] Bytes { get; init; }
}