using System.Reflection;
using Admission.Dictionary.Application.Context;
using Admission.Dictionary.Domain.Entities;
using Admission.OutboxMessages.Context;
using Admission.OutboxMessages.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Infrastructure;

public sealed class DictionaryDbContext: DbContext, IDictionaryDbContext, IOutboxMessageDbContext
{
    public DbSet<EducationDocumentType> DocumentTypes { get; init; }
    public DbSet<EducationLevel> EducationLevels { get; init; }
    public DbSet<EducationProgram> Programs { get; init; }
    public DbSet<Faculty> Faculties { get; init; }

    public DbSet<NextEducationLevel> NextEducationLevels { get; init; }
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    
    public DictionaryDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}