using System.Linq.Expressions;

namespace Infrastructure;

public interface IAsyncRepository<T> where T : class
{
    Task<T> GetByIdAsync<TPrimaryKey>(TPrimaryKey id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleBySpec(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<List<T>> AddListAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> ExecQueryAsync(string query);
}