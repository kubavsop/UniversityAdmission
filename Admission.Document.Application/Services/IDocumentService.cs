using Admission.Application.Common.Result;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;

namespace Admission.Document.Application.Services;

public interface IDocumentService
{
    Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId);
    Task<Result<PassportDto>> GetPassportAsync(Guid userId);
    Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId);
    Task<Result> CreateEducationDocumentAsync(CreateEducationDocumentDto documentDto, Guid userId);
    Task<Result<IEnumerable<EducationDocumentDto>>> GetEducationDocumentAsync(Guid userId);
    Task<Result> EditEducationDocumentAsync(EditEducationDocumentDto documentDto, Guid documentId, Guid userId);
    Task<Result> DeleteEducationDocumentAsync(Guid documentId, Guid userId);
    Task<Result> AddScan(Guid userId, Guid documentId, CreateScanDto createScanDto);
}