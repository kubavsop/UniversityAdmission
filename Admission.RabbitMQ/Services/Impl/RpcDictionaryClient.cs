using Admission.Application.Common.Services;
using Admission.DTOs.RpcModels.DocumentType;
using Admission.DTOs.RpcModels.EducationLevel;
using Admission.DTOs.RpcModels.Faculty;
using Admission.DTOs.RpcModels.Program;
using Admission.RabbitMQ.Options;
using Admission.RabbitMQ.Services.Base;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Admission.RabbitMQ.Services.Impl;

public sealed class RpcDictionaryClient: BaseRpcClient, IRpcDictionaryClient
{
    private const string QueueName = "DictionaryRpcQueue";

    public RpcDictionaryClient(IOptions<RpcClientQueueNameOptions> queueName, IConnection connection): base(QueueName, queueName.Value.Name, connection)
    {
    }

    public async Task<FacultyResponse?> GetFacultyByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetFacultyRequest { Id = id });
        return result as FacultyResponse;
    }

    public async Task<ProgramResponse?> GetProgramByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetProgramRequest { Id = id });
        return result as ProgramResponse;
    }

    public async Task<EducationLevelResponse?> GetEducationLevelByIdAsync(int id)
    {
        var result = await CallAsync(new GetEducationLevelRequest { Id = id });
        return result as EducationLevelResponse;
    }

    public async Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(Guid id)
    {
        var result = await CallAsync(new GetDocumentTypeRequest { Id = id });
        return result as DocumentTypeResponse;
    }
}