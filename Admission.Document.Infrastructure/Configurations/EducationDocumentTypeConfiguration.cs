using Admission.Document.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Document.Infrastructure.Configurations;

internal sealed class EducationDocumentTypeConfiguration: IEntityTypeConfiguration<EducationDocumentType>
{
    public void Configure(EntityTypeBuilder<EducationDocumentType> builder)
    {
        builder
            .HasMany(t => t.EducationLevels)
            .WithMany(l => l.DocumentTypes)
            .UsingEntity<NextEducationLevel>(
                l => l.HasOne<EducationLevel>(e => e.EducationLevel).WithMany(e => e.NextEducationLevels).HasPrincipalKey(e => e.ExternalId));

        builder
            .HasOne(t => t.EducationLevel)
            .WithMany()
            .HasPrincipalKey(t => t.ExternalId)
            .IsRequired();
    }
}