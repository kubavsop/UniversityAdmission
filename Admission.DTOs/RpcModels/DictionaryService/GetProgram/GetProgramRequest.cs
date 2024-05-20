using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DictionaryService.GetProgram;

public class GetProgramRequest: BaseDto, IRpcRequest<ProgramResponse?>;