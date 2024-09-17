using Application.ViewModels;
using Domain.Entities.Application;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<UvwTransactionInfo>> GetTransactionInfo(DateTime from, DateTime to);
}