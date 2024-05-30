using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantEducationDocuments;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class EducationDocumentViewModel: DocumentViewModel, IMapFrom<EducationDocumentResponse>
{
    [Required(ErrorMessage = "Поле обязательно")]
    [MinLength(1, ErrorMessage = "Минимальная длина 1 символ")]
    [MaxLength(200, ErrorMessage = "Максимальная длина 200 символов")]
    public required string Name { get; init; }
    
    [Required(ErrorMessage = "Поле обязательно")]
    public required Guid EducationDocumentTypeId { get; init; }
    
    public required IEnumerable<ScanRpcModel> Scans { get; init; }
    public required bool IsEditable { get; init; }
}