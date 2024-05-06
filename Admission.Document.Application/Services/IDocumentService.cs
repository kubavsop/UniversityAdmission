using Admission.Application.Common.Result;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;

namespace Admission.Document.Application.Services;

public interface IDocumentService
{
    Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId);
    Task<Result<PassportDto>> GetPassportAsync(Guid userId);
    Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId);
}