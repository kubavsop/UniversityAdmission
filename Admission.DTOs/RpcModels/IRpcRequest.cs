using MediatR;

namespace Admission.DTOs.RpcModels;

public interface IRpcRequest<out T> : IRequest<T> where T: IRpcResponse?;