using System.Reflection;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Infrastructure;

public sealed class DictionaryDbContext: DbContext, IDictionaryDbContext
{
    public DbSet<EducationDocumentType> DocumentTypes { get; init; }
    public DbSet<EducationLevel> EducationLevels { get; init; }
    public DbSet<EducationProgram> Programs { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    
    public DictionaryDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}