using Admission.Domain.Common.Enums;
using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admission.User.Infrastructure.Configurations;

internal sealed class AdmissionRoleConfiguration: IEntityTypeConfiguration<AdmissionRole>
{
    public void Configure(EntityTypeBuilder<AdmissionRole> builder)
    {
        builder.HasData(GetDefaultAdmissionRoles());
    }
    
    private static IEnumerable<AdmissionRole> GetDefaultAdmissionRoles() =>
        Enum.GetValues<RoleType>().Select(type =>
        {
            var name = type.ToString();
            return new AdmissionRole
            {
                Id = Guid.NewGuid(),
                Type = type,
                Name = name,
                NormalizedName = name.ToUpper()
            };
        });
}