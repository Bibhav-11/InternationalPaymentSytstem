using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EFRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    private DbSet<T> entities;
    
    public EFRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        entities = _dbContext.Set<T>();
    }
    public async Task<T> GetByIdAsync<TPrimaryKey>(TPrimaryKey id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T> GetSingleBySpec(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<List<T>> AddListAsync(List<T> entity)
    {
        await _dbContext.Set<T>().AddRangeAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> ExecQueryAsync(string query)
    {
        return await _dbContext.Set<T>().FromSqlRaw(query).ToListAsync();
    }
}