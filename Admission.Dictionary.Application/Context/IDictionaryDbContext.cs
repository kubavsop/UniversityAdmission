using System.Diagnostics.CodeAnalysis;
using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Admission.Dictionary.Application.Context;

public interface IDictionaryDbContext
{
    DbSet<EducationDocumentType> DocumentTypes{ get; }
    DbSet<EducationLevel> EducationLevels { get; }
    DbSet<Faculty> Faculties { get; init; }
    DbSet<EducationProgram> Programs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : class;
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}