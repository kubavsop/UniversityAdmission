using Admission.Document.Domain.Enums;
using Admission.Document.Domain.Events.Passport;

namespace Admission.Document.Domain.Entities;

public sealed class Passport : Document
{
    public int Series { get; private set; }
    public int Number { get; private set; }
    public string PlaceOfBirth { get; private set; }
    public string IssuedBy { get; private set; }
    public DateTime DateIssued { get; private set; }

    private Passport()
    {
    }

    public static Passport CreatePassport(
        int series,
        int number,
        string placeOfBirth,
        string issuedBy,
        DateTime dateIssued,
        Guid applicantId)
    {
        var passport = new Passport
        {
            Id = Guid.NewGuid(),
            Series = series,
            Number = number,
            PlaceOfBirth = placeOfBirth,
            IssuedBy = issuedBy,
            DateIssued = dateIssued,
            ApplicantId = applicantId,
            Type = DocumentType.Passport
        };
        
        passport.AddDomainEvent(new PassportChangedDomainEvent(passport));

        return passport;
    }

    public void ChangeSeries(int series)
    {
        if (Series == series) return;
        Series = series;

        AddDomainEvent(new PassportChangedDomainEvent(this));
    }

    public void ChangeNumber(int number)
    {
        if (Number == number) return;
        Number = number;

        AddDomainEvent(new PassportChangedDomainEvent(this));
    }

    public void ChangePlaceOfBirth(string placeOfBirth)
    {
        if (PlaceOfBirth == placeOfBirth) return;
        PlaceOfBirth = placeOfBirth;

        AddDomainEvent(new PassportChangedDomainEvent(this));
    }

    public void ChangeIssuedBy(string issuedBy)
    {
        if (IssuedBy == issuedBy) return;
        IssuedBy = issuedBy;

        AddDomainEvent(new PassportChangedDomainEvent(this));
    }

    public void ChangeDateIssued(DateTime dateIssued)
    {
        if (DateIssued == dateIssued) return;
        DateIssued = dateIssued;

        AddDomainEvent(new PassportChangedDomainEvent(this));
    }
}