using Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Infrastructure.Configurations;

internal sealed class ApplicantConfiguration: IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
       builder
           .HasMany(a => a.EducationLevels)
           .WithMany(l => l.Applicants)
           .UsingEntity<NextEducationLevel>(
               l => l
                   .HasOne<EducationLevel>(e => e.EducationLevel)
                   .WithMany(e => e.NextEducationLevels)
                   .HasPrincipalKey(e => e.ExternalId));
    }
}