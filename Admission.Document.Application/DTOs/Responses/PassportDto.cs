using Admission.Application.Common.Mapping;
using Admission.Document.Domain.Entities;

namespace Admission.Document.Application.DTOs.Responses;

public sealed class PassportDto: IMapFrom<Passport>
{
    public Guid Id { get; set; }
    public int Series { get; set; }
    public int Number { get; set; }
    public required string PlaceOfBirth { get; set; }
    public required string IssuedBy { get; set; }
    public DateTime DateIssued { get; set; }
    public ICollection<FileDto> Files { get; } = new List<FileDto>();
}