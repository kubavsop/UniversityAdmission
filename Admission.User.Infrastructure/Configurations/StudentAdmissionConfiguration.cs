using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.Configurations;

internal sealed class StudentAdmissionConfiguration: IEntityTypeConfiguration<StudentAdmission>
{
    public void Configure(EntityTypeBuilder<StudentAdmission> builder)
    {
        builder.HasOne(a => a.FirstPriorityFaculty)
            .WithMany()
            .HasForeignKey(a => a.FirstPriorityFacultyId);
    }
}