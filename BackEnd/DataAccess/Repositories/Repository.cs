using System.Linq.Expressions;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    
    public async Task<IList<T>> QueryAsync(
        Expression<Func<T, bool>>? filter = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, 
        int? take = null, int skip = 0,
        bool asNoTracking = false)
    {
        var query = DbSet.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();
            
        if (include is not null)
            query = include(query);
            
        if (filter is not null)
            query = query.Where(filter);
            
        if (orderBy is not null)
            query = orderBy(query);
            
        query = query.Skip(skip);

        if (take is not null)
            query = query.Take(take.Value);
            
        return await query.ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, 
        bool asNoTracking = false)
    {
        var query = await QueryAsync(
            filter: filter,
            include: include,
            asNoTracking: asNoTracking
        );
            
        return query.FirstOrDefault();
    }

    public async Task CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
            DbContext.Attach(entity);

        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
            DbContext.Attach(entity);

        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}