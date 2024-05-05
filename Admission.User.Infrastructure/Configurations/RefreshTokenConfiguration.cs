using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.Configurations;

internal sealed class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
            .HasIndex(rt => rt.Token)
            .IsUnique();

        builder
            .HasIndex(rt => rt.AccessTokenId)
            .IsUnique();
    }
}