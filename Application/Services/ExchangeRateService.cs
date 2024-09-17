using Application.Interfaces;
using Application.ViewModels;
using Infrastructure.HttpClients.NRBExchangeRate;

namespace Application.Services;

public class ExchangeRateService: IExchangeRateService
{
    private readonly IExchangeRateClient _exchangeRateClient;

    public ExchangeRateService(IExchangeRateClient exchangeRateClient)
    {
        _exchangeRateClient = exchangeRateClient;
    }

    public async Task<IEnumerable<ExchangeRateViewModel>> GetExchangeRate(DateTime date)
    {
        DateTime from;
        DateTime to;
        from = to = date;
        IEnumerable<ExchangeRateViewModel> model = new List<ExchangeRateViewModel>();
        var response = await _exchangeRateClient.GetExchangeRate(from, to);
        if (response.Data != null && response.Data.Payload.Count > 0)
        {
            var payload = response.Data.Payload[0];
            model = payload.Rates.Select(x => new ExchangeRateViewModel()
            {
                Currency = $"{x?.Currency?.Iso3} ({x?.Currency?.Name})",
                Unit = x.Currency.Unit,
                Buy = x.Buy,
                Sell = x.Sell,
                CurrencyCode = x.Currency.Iso3
            });
        }

        return model;
    }
}