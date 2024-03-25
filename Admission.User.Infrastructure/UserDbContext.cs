using Admission.Infrastructure.Common.Contexts;
using Admission.User.Domain.Entities;
using Admission.User.Infrastructure.DbConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Infrastructure;


public class UserDbContext: BaseIdentityContext<AdmissionUser, Role, Guid, IdentityUserClaim<Guid>,IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Applicant> Applicants { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    public DbSet<Manager> Managers { get; init; }
    
    public UserDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicantConfiguration());
        builder.ApplyConfiguration(new ManagerConfiguration());
    }
}