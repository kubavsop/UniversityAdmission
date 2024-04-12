using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Dictionary.Infrastructure.Configurations;

internal sealed class EducationDocumentConfiguration: IEntityTypeConfiguration<EducationDocumentType>
{
    public void Configure(EntityTypeBuilder<EducationDocumentType> builder)
    {
        builder
            .HasMany(t => t.NextEducationLevels)
            .WithMany(l => l.DocumentTypes);

        builder
            .HasOne(t => t.EducationLevel)
            .WithMany()
            .IsRequired();
    }
}