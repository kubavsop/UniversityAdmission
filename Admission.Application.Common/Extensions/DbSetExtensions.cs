using Admission.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Common.Extensions;

public static class DbSetExtensions
{
    public static IQueryable<T> GetUndeleted<T>(this DbSet<T> dbSet) where T : class, IBaseEntity
    {
        return dbSet.Where(e => !e.DeleteTime.HasValue);
    }

    public static async Task<T?> GetByIdAsync<T>(this DbSet<T> dbSet, Guid id) where T : class, IBaseEntity
    {
        return await dbSet
            .GetUndeleted()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public static IQueryable<T> GetUndeleted<T>(this IQueryable<T> dbSet) where T : class, IBaseEntity
    {
        return dbSet.Where(e => !e.DeleteTime.HasValue);
    }

    public static async Task<T?> GetByIdAsync<T>(this IQueryable<T> dbSet, Guid id) where T : class, IBaseEntity
    {
        return await dbSet
            .GetUndeleted()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}