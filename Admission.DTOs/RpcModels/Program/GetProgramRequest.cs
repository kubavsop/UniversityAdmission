using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.Program;

public class GetProgramRequest: BaseDto, IRpcRequest<ProgramResponse?>;