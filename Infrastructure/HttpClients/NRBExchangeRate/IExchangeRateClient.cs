using Infrastructure.HttpClients.NRBExchangeRate.ResponseModels;

namespace Infrastructure.HttpClients.NRBExchangeRate;

public interface IExchangeRateClient
{
    Task<ExchangeRateResponseModel> GetExchangeRate(DateTime fromDate, DateTime toDate);
}