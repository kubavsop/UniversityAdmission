using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.DbConfigurations;

public class ApplicantConfiguration: IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Applicant>(a => a.Id);

        builder.HasOne(a => a.FirstPriorityFaculty)
            .WithMany()
            .HasForeignKey(a => a.FirstPriorityFacultyId);
    }
}