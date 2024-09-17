using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternationalPaymentTransfer.Controllers;

public class ExchangeRateController: Controller
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task<IActionResult> ExchangeRatesToday()
    {
        var response = await _exchangeRateService.GetExchangeRate(DateTime.Now);
        return Json(new {data =response});
    }
}