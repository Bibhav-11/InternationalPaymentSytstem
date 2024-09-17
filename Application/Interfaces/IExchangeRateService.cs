using Application.ViewModels;

namespace Application.Interfaces;

public interface IExchangeRateService
{
    Task<IEnumerable<ExchangeRateViewModel>> GetExchangeRate(DateTime date);
}