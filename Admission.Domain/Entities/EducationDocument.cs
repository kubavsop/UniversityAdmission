using Admission.Domain.Common.Entities;

namespace Admission.Domain.Entities;

public sealed class EducationDocument: BaseEntity
{
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
    public Guid EducationDocumentTypeId { get; set; }
    public EducationDocumentType EducationDocumentType { get; set; } = null!;
}