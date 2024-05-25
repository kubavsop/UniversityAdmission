using Admission.Application.Common.Result;
using Admission.Application.DTOs.Requests;

namespace Admission.Application.Services;

public interface IProgramService
{
    Task<Result> CreateProgramAsync(CreateProgramDto createProgramDto, Guid userId, bool isManager = false);
    Task<Result> EditProgramsAsync(EditProgramsDto editProgramDto, Guid userId, bool isManager = false);
    Task<Result> DeleteProgramAsync(Guid programId, Guid userId, bool isManager = false);
}