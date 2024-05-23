using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;

namespace Admission.DTOs.RpcModels.DictionaryService.GetFaculties;

public sealed class FacultiesResponse: IRpcResponse
{
    public required IEnumerable<FacultyResponse> Faculties { get; init; }
}