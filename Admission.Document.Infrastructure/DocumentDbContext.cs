using System.Reflection;
using Admission.Document.Application.Context;
using Admission.Document.Domain.Entities;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using File = Admission.Document.Domain.Entities.File;

namespace Admission.Document.Infrastructure;

public class DocumentDbContext: DbContext, IDocumentDbContext, IOutboxMessageDbContext
{
    public DbSet<Applicant> Applicants { get; init; }
    public DbSet<EducationDocument> EducationDocuments { get; init; }
    public DbSet<EducationDocumentType> EducationDocumentTypes { get; init; }
    public DbSet<EducationLevel> EducationLevels { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    public DbSet<File> Files { get; init; }
    public DbSet<Manager> Managers { get; init; }
    public DbSet<NextEducationLevel> NextEducationLevels { get; init; }
    public DbSet<Passport> Passports { get; init; }
    public DbSet<StudentAdmission> StudentAdmissions { get; init; }
    
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    
    public DocumentDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}