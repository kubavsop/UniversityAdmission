using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.DownloadScan;

public sealed class DownloadScanRequest: AuthorizedRequest, IRpcRequest<ScanRpcFullModelResponse>
{
    public Guid ScanId { get; init; }
}