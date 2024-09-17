using System.Net;
using Infrastructure.HttpClients.NRBExchangeRate.ResponseModels;
using Newtonsoft.Json;

namespace Infrastructure.HttpClients.NRBExchangeRate;

public class ExchangeRateClient: IExchangeRateClient
{
    private readonly HttpClient _httpClient;

    public ExchangeRateClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateResponseModel> GetExchangeRate(DateTime fromDate, DateTime toDate)
    {
        try
        {
            string from = fromDate.ToString("yyyy-MM-dd");
            string to = toDate.ToString("yyyy-MM-dd");
            
            var requestUri = $"forex/v1/rates?page=1&per_page=10&from={from}&to={to}";
            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var exchangeRateResponse = JsonConvert.DeserializeObject<ExchangeRateResponseModel>(jsonString);
                if (exchangeRateResponse?.Status?.Code == 200) return exchangeRateResponse;
                return new ExchangeRateResponseModel();
            }

            return new ExchangeRateResponseModel();
        }
        catch (Exception ex)
        {
            return new ExchangeRateResponseModel();
        }
    }
}