using Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Infrastructure.Configurations;

internal sealed class ProgramConfiguration: IEntityTypeConfiguration<EducationProgram>
{
    public void Configure(EntityTypeBuilder<EducationProgram> builder)
    {
        builder.HasMany(p => p.StudentAdmissions)
            .WithMany(sa => sa.EducationPrograms)
            .UsingEntity<AdmissionProgram>();
        
        builder
            .HasOne(p => p.EducationLevel)
            .WithMany()
            .HasPrincipalKey(l => l.ExternalId)
            .IsRequired();
    }
}