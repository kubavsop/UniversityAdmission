using Admission.Application.Common.Mapping;
using Admission.Document.Domain.Entities;

namespace Admission.Document.Application.DTOs.Responses;

public sealed class EducationDocumentDto: IMapFrom<EducationDocument>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public EducationDocumentTypeDto EducationDocumentType { get; set; }
    public ICollection<FileDto> Files { get; } = new List<FileDto>();
}