namespace Admission.Application.Common.Exceptions;

public class RpcException : Exception
{
    public RpcException() {}
    
    public RpcException(string message) : base(message) {}
}