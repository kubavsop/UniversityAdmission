using Admission.Dictionary.Application.Services;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.DictionaryService.GetUpdateStatus;
using MediatR;

namespace Admission.Dictionary.Application.RpcHandlers;

public sealed class GetUpdateStatusRequestHandler: IRequestHandler<GetUpdateStatusRequest, IRpcResponse>
{
    private readonly IUpdateStatusService _updateStatusService;

    public GetUpdateStatusRequestHandler(IUpdateStatusService updateStatusService)
    {
        _updateStatusService = updateStatusService;
    }

    public async Task<IRpcResponse> Handle(GetUpdateStatusRequest request, CancellationToken cancellationToken)
    {
        return await _updateStatusService.GetUpdateStatusAsync();
    }
}