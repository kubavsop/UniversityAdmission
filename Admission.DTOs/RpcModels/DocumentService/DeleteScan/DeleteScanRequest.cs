using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.DeleteScan;

public sealed class DeleteScanRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid ScanId { get; init; }
}