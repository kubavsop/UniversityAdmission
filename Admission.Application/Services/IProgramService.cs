using Admission.Application.Common.Result;
using Admission.Application.DTOs.Requests;

namespace Admission.Application.Services;

public interface IProgramService
{
    Task<Result> CreateProgramAsync(CreateProgramDto createProgramDto, Guid userId);
    Task<Result> EditProgramsAsync(EditProgramsDto editProgramDto, Guid userId);
    Task<Result> DeleteProgramAsync(Guid programId, Guid userId);
}