namespace Admission.Document.Domain.Entities;

public sealed class Passport: Document
{
    public int Series { get; set; }
    public int Number { get; set; }
    public DateTime PlaceOfBirth { get; set; }
    public required string IssuedBy { get; set; }
    public DateTime DateIssued { get; set; }
}