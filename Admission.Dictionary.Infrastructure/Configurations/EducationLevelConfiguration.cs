using System.Reflection.Metadata;
using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Dictionary.Infrastructure.Configurations;

public sealed class EducationLevelConfiguration: IEntityTypeConfiguration<EducationLevel>
{
    public void Configure(EntityTypeBuilder<EducationLevel> builder)
    {
        builder.HasAlternateKey(l => l.ExternalId);
    }
}