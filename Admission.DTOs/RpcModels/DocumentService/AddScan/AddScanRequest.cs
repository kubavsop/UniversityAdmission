using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.AddScan;

public sealed class AddScanRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid DocumentId { get; init; }
    public required ScanRpcModel ScanModel { get; init; }
}