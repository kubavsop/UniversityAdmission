using Admission.Application.Common.Result;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;

namespace Admission.Document.Application.Services;

public interface IDocumentService
{
    Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId);
    Task<Result<PassportDto>> GetPassportAsync(Guid userId);
    Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId);
    Task<Result> CreateEducationDocument(CreateEducationDocumentDto documentDto, Guid userId);
    Task<Result<EducationDocumentDto>> GetEducationDocument(Guid userId);
    Task<Result> EditEducationDocument(EditEducationDocumentDto documentDto, Guid documentId, Guid userId);
    Task<Result> DeleteEducationDocument(Guid documentId, Guid userId);
}