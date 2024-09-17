using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternationalPaymentTransfer.Controllers;

public class ProfileController: Controller
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> ProfileInfo()
    {
        var model = await _profileService.GetProfileInfo();
        ViewData["Currency"] = await _profileService.GetCurrency();
        return PartialView("_ProfileInfo", model);
    }

    [HttpGet]
    public IActionResult AddAccount()
    {
        return PartialView("_AddAccount");
    }

    [HttpPost]
    public async Task<IActionResult> AddAccount(BankDetailsModel model)
    {
        var response = await _profileService.AddAccount(model);
        if (response)
        {
            return Json(new { Success = true, Message = "Successfully added the bank account to your profile" });
        }
        else
        {
            return Json(new { Success = false, Message = "Sorry! Something went wrong" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> LoadBalance(int? id, decimal? balance)
    {
        if (id is null || balance is null) return BadRequest("Both Bank Account Id and Balance are required");
        if (!await _profileService.AccountBelongsToUser(id ?? 0)) return new ForbidResult();

        var response = await _profileService.LoadBalance(id ?? 0, balance ?? 0);
        if (response)
        {
            return Json(new { Success = true, Message = $"Successfully loaded {balance} amount to the account" });
        }
        else
        {
            return Json(new { Success = false, Message = "Sorry! Something went wrong" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> TransferAmount(int id)
    {
        var transferModel = await _profileService.GenerateTransferModel(id);
        return PartialView("_TransferAmount", transferModel);
    }

    [HttpGet]
    public async Task<IActionResult> TransferConfirmation(int id, TransferAmountViewModel model)
    {
        if (!await _profileService.AccountBelongsToUser(id)) return new ForbidResult();

        var accountExists = await _profileService.AccountExists(id, model);
        if (!accountExists) return BadRequest();

        if (!await _profileService.BalanceEnough(id, model.Amount)) return BadRequest();

        var confirmModel = await _profileService.GetTransferModel(id, model);

        return PartialView("_ConfirmTransfer", confirmModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> ConfirmTransfer(int id, TransferAmountViewModel model)
    {
        if (!await _profileService.AccountBelongsToUser(id)) return new ForbidResult();

        var accountExists = await _profileService.AccountExists(id, model);
        if (!accountExists) return BadRequest();

        if (!await _profileService.BalanceEnough(id, model.Amount)) return BadRequest();

        var response = await _profileService.ConfirmTransfer(id, model);

        return Json(response);
    }
}