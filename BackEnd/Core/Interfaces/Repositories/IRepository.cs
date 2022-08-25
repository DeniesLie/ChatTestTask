using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Interfaces.Repositories;

public interface IRepository<T> 
    where T : class
{
    public Task<IList<T>> QueryAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int? take = null, int skip = 0,
        bool asNoTracking = false);
    
    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool asNoTracking = false);
    
    Task CreateAsync(T entity);
        
    void Update(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}