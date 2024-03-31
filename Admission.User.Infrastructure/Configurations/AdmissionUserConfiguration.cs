using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.Configurations;

public class AdmissionUserConfiguration: IEntityTypeConfiguration<AdmissionUser>
{
    public void Configure(EntityTypeBuilder<AdmissionUser> builder)
    {
        builder
            .HasIndex(u => u.RefreshToken)
            .IsUnique();
    }
}