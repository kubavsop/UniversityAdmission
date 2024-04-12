using Admission.Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Dictionary.Application.Context;

public interface IDictionaryDbContext
{
    DbSet<EducationDocumentType> DocumentTypes{ get; }
    DbSet<EducationLevel> EducationLevels { get; }
    DbSet<Faculty> Faculties { get; init; }
    DbSet<EducationProgram> Programs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}