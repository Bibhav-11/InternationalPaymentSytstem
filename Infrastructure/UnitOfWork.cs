using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationContext _context;
    
    
    public Dictionary<Type, object> Repositories = new();

    public UnitOfWork(ApplicationContext context)
    {
        _context = context;
    }
    
    public IAsyncRepository<T> AsyncRepository<T>() where T : class
    {
        if (Repositories.ContainsKey(typeof(T)))
        {
            return Repositories[typeof(T)] as IAsyncRepository<T>;
        }
        IAsyncRepository<T>  repo = new EFRepository<T>(_context);
        Repositories.Add(typeof(T), repo);
        return repo;
    }

    public async Task<bool> CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            //Log exception
            return false;
        }
    }
}