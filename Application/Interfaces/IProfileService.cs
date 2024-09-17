using Application.ViewModels;

namespace Application.Interfaces;

public interface IProfileService
{
    Task<ProfileInfoViewModel> GetProfileInfo();
    Task<bool> AddAccount(BankDetailsModel model);
    Task<string> GetCurrency();
    Task<bool> AccountBelongsToUser(int bankAccountId);
    Task<bool> LoadBalance(int bankAccountId, decimal balance);
    Task<TransferAmountViewModel> GenerateTransferModel(int bankAccountId);
    Task<bool> AccountExists(int checkAccount, TransferAmountViewModel model);
    Task<bool> BalanceEnough(int checkAccount, decimal? amount);
    Task<ConfirmTransferModel> GetTransferModel(int fromId, TransferAmountViewModel model);
    Task<bool> ConfirmTransfer(int fromId, TransferAmountViewModel model);
}