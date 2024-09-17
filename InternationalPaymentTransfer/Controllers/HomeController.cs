using System.Diagnostics;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using InternationalPaymentTransfer.Models;
using Microsoft.AspNetCore.Authorization;

namespace InternationalPaymentTransfer.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUserService _currentUserService;

    public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}