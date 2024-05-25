using Admission.Application.Common.Result;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;

namespace Admission.Document.Application.Services;

public interface IDocumentService
{
    Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId, bool isManager = false);
    Task<Result<PassportDto>> GetPassportAsync(Guid userId);
    Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId, bool isManager = false);
    Task<Result> CreateEducationDocumentAsync(CreateEducationDocumentDto documentDto, Guid userId, bool isManager = false);
    Task<Result<IEnumerable<EducationDocumentDto>>> GetEducationDocumentAsync(Guid userId);
    Task<Result> EditEducationDocumentAsync(EditEducationDocumentDto documentDto, Guid documentId, Guid userId, bool isManager = false);
    Task<Result> DeleteEducationDocumentAsync(Guid documentId, Guid userId, bool isManager = false);
    Task<Result> AddScan(Guid userId, Guid documentId, CreateScanDto createScanDto, bool isManager = false);
}