using System.Reflection;
using Admission.Application.Context;
using Admission.Domain.Entities;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Admission.Infrastructure;

public sealed class AdmissionDbContext : DbContext, IAdmissionDbContext, IOutboxMessageDbContext
{
    public DbSet<EducationDocument> EducationDocuments { get; init; }
    public DbSet<EducationDocumentType> EducationDocumentTypes { get; init; }
    public DbSet<AdmissionProgram> AdmissionPrograms { get; init; }
    public DbSet<Applicant> Applicants { get; init; }
    public DbSet<EducationLevel> EducationLevels { get; init; }
    public DbSet<EducationProgram> EducationPrograms { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    public DbSet<Manager> Managers { get; init; }
    public DbSet<NextEducationLevel> NextEducationLevels { get; init; }
    public DbSet<StudentAdmission> StudentAdmissions { get; init; }
    public DbSet<AdmissionGroup> AdmissionGroups { get; init; }
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    
    public AdmissionDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}