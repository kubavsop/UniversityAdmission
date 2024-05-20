namespace Admission.DTOs.RpcModels.Base;

public sealed class RpcErrorResponse : IRpcResponse
{
    public RpcErrorResponse()
    {
    }
    public RpcErrorResponse(string message)
    {
        Message = message;
    }
    
    public string Message { get; init; }
};