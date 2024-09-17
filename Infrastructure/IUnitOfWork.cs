namespace Infrastructure;

public interface IUnitOfWork
{
    IAsyncRepository<T> AsyncRepository<T>() where T: class;
    Task<bool> CommitAsync();
}