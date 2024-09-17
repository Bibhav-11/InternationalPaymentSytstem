namespace Infrastructure.HttpClients.NRBExchangeRate.ResponseModels;

public class ExchangeRateResponseModel
{
    public Status Status { get; set; }
    public Errors Errors { get; set; }
    public Params Params { get; set; }
    public Data Data { get; set; }
    public Pagination Pagination { get; set; }
}