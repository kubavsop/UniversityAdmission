using Admission.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public static IQueryable<T1> GetUndeleted<T1, T2>(this IIncludableQueryable<T1, T2> dbSet) where T1 : IBaseEntity where T2: IBaseEntity
    {
        return dbSet.Where(e => !e.DeleteTime.HasValue);
    }

    public static IEnumerable<T> GetUndeleted<T>(this ICollection<T> collection) where T : class, IBaseEntity
    {
        return collection.Where(e => !e.DeleteTime.HasValue);
    }

    public static async Task<T?> GetByIdAsync<T>(this IQueryable<T> dbSet, Guid id) where T : class, IBaseEntity
    {
        return await dbSet
            .GetUndeleted()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}