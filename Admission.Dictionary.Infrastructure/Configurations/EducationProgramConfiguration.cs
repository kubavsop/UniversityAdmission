using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.Dictionary.Infrastructure.Configurations;

public sealed class EducationProgramConfiguration: IEntityTypeConfiguration<EducationProgram>
{
    
    public void Configure(EntityTypeBuilder<EducationProgram> builder)
    {
        builder
            .HasOne(p => p.EducationLevel)
            .WithMany()
            .HasForeignKey(p => p.EducationLevelId);
    }
}