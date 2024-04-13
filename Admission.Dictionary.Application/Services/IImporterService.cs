using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;

namespace Admission.Dictionary.Application.Services;

public interface IImporterService
{
    Task<ProgramPagedListDto> GetTest();
}