using Admission.Application.Common.Mapping;
using Admission.Document.Domain.Entities;

namespace Admission.Document.Application.DTOs.Responses;

public sealed class EducationDocumentDto: IMapFrom<EducationDocumentType>
{
    public required string Name { get; set; }
    public Guid EducationDocumentTypeId { get; set; }
    public EducationDocumentTypeDto EducationDocumentType { get; set; } = null!;
}