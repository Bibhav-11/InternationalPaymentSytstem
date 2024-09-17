using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities.Application;
using Infrastructure;

namespace Application.Services;

public class TransactionService: ITransactionService
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;
    private readonly int _currentUserId;

    public TransactionService(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
        _currentUserId = _currentUserService.UserId ?? 0;
    }

    public async Task<IEnumerable<UvwTransactionInfo>> GetTransactionInfo(DateTime from, DateTime to)
    {
        DateTime startOfDay = from.Date;
    
        DateTime endOfDay = to.Date.AddDays(1).AddTicks(-1);

        var lists = await _uow.AsyncRepository<UvwTransactionInfo>().ListAsync(x =>
            (x.SenderUserId == _currentUserId || x.ReceiverUserId == _currentUserId) &&
            x.TransactionDate >= startOfDay &&
            x.TransactionDate <= endOfDay);
        
        return lists;
    }
}