using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternationalPaymentTransfer.Controllers;

public class TransactionsController: Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetTransactionData(DateTime from, DateTime to)
    {
        return Json(new {data =await _transactionService.GetTransactionInfo(from, to)});
    }
}