using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.Configurations;

internal sealed class ApplicantConfiguration: IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Applicant>(a => a.Id);
        
        builder.HasOne(a => a.StudentAdmission)
            .WithOne(sa => sa.Applicant)
            .HasForeignKey<StudentAdmission>(sa => sa.ApplicantId)
            .IsRequired();
    }
}