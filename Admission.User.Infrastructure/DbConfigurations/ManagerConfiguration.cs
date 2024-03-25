using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.DbConfigurations;

public class ManagerConfiguration: IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasOne(m => m.User)
            .WithOne()
            .HasForeignKey<Manager>(m => m.Id);
    }
}