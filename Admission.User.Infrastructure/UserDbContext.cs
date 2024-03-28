using Admission.User.Domain.Entities;
using Admission.User.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Infrastructure;


public sealed class UserDbContext: IdentityDbContext<AdmissionUser, AdmissionRole, Guid>
{
    public DbSet<Applicant> Applicants { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    public DbSet<Manager> Managers { get; init; }
    public DbSet<StudentAdmission> StudentAdmissions { get; init; }
    
    public UserDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicantConfiguration());
        builder.ApplyConfiguration(new ManagerConfiguration());
        builder.ApplyConfiguration(new StudentAdmissionConfiguration());
    }
}