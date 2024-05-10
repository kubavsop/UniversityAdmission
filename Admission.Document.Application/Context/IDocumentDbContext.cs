using Admission.Document.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.Context;

public interface IDocumentDbContext
{
    public DbSet<Domain.Entities.Document> Documents { get; }
    public DbSet<Applicant> Applicants { get; }
    public DbSet<EducationDocument> EducationDocuments { get; }
    public DbSet<EducationDocumentType> EducationDocumentTypes { get; }
    public DbSet<EducationLevel> EducationLevels { get; }
    public DbSet<Faculty> Faculties { get; }
    public DbSet<Domain.Entities.File> Files { get; }
    public DbSet<Manager> Managers { get; init; }
    public DbSet<NextEducationLevel> NextEducationLevels { get; }
    public DbSet<Passport> Passports { get; }
    public DbSet<StudentAdmission> StudentAdmissions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}